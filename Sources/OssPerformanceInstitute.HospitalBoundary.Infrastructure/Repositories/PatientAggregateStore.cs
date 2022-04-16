using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OssPerformanceInstitute.HospitalBoundary.Domain.Entities;
using OssPerformanceInstitute.HospitalBoundary.Domain.Repositories;
using OssPerformanceInstitute.HospitalBoundary.Domain.ValueObjects;
using OssPerformanceInstitute.HospitalBoundary.Infrastructure.Dtos;
using OssPerformanceInstitute.SharedKernel.Common;

namespace OssPerformanceInstitute.HospitalBoundary.Infrastructure.Repositories
{
    public class PatientAggregateStore : IPatientAggregateStore
    {
        private readonly CosmosClient cosmosClient;
        private readonly Container patientContainer;
        public PatientAggregateStore(IConfiguration configuration)
        {
            var connectionString = configuration["CosmosDb:ConnectionString"];
            var databaseId = configuration["CosmosDb:DatabaseId"];
            var containerId = configuration["CosmosDb:ContainerId"];

            var clientOptions = new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };

            cosmosClient = new CosmosClient(connectionString, clientOptions);
            patientContainer = cosmosClient.GetContainer(databaseId, containerId);
        }
        public async Task<Patient> LoadAsync(PatientId patientId)
        {
            if (patientId == null)
            {
                throw new ArgumentNullException(nameof(patientId));
            }

            var aggregateId = $"Patient-{patientId.Value}";
            var sqlQueryText = $"SELECT * FROM c WHERE c.aggregateId = '{aggregateId}'";
            var queryDefinition = new QueryDefinition(sqlQueryText);
            var queryResultSetIterator = patientContainer.GetItemQueryIterator<EventData>(queryDefinition);
            var allEvents = new List<EventData>();

            while (queryResultSetIterator.HasMoreResults)
            {
                var currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (EventData item in currentResultSet)
                {
                    allEvents.Add(item);
                }
            }

            var domainEvents = allEvents.Select(e =>
            {
                var assemblyQualifiedName = JsonConvert.DeserializeObject<string>(e.AssemblyQualifiedName);
                if (string.IsNullOrEmpty(assemblyQualifiedName))
                    throw new ArgumentNullException($"{nameof(assemblyQualifiedName)} is null or empty");
                
                var eventType = Type.GetType(assemblyQualifiedName);
                if (eventType == null)
                    throw new ArgumentNullException($"{nameof(eventType)} is null");
                var data = JsonConvert.DeserializeObject(e.Data, eventType);
                return data as IDomainEvent;
            });

            var aggregate = new Patient();

            aggregate.Load(domainEvents!);

            return aggregate;
        }

        public async Task SaveAsync(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }

            var changes = patient.GetChanges()
              .Select(e => new EventData(Guid.NewGuid(),
                                               $"Patient-{patient.Id}",
                                               e.GetType().Name,
                                               JsonConvert.SerializeObject(e),
                                               JsonConvert.SerializeObject(e.GetType().AssemblyQualifiedName)))
              .AsEnumerable();

            if (!changes.Any())
            {
                return;
            }

            foreach (var item in changes)
            {
                await patientContainer.CreateItemAsync(item);
            }

            patient.ClearChanges();
        }
    }
}
