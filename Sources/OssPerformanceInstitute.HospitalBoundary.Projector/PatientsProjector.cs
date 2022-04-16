using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Data.SqlClient;
using OssPerformanceInstitute.HospitalBoundary.Domain.Repositories;
using OssPerformanceInstitute.HospitalBoundary.Domain.ValueObjects;
using OssPerformanceInstitute.HospitalBoundary.Infrastructure.Dtos;
using OssPerformanceInstitute.HospitalBoundary.Projector.Extensions;

namespace OssPerformanceInstitute.HospitalBoundary.Projector
{
    /// <summary>
    /// Application pattern Event Sourcing to hydrate Patient Entity
    /// </summary>
    public class PatientsProjector : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IPatientAggregateStore _aggregateStore;
        private readonly CosmosClient _cosmosClient;
        private ChangeFeedProcessor? _changeFeedProcessor;

        private static IConfiguration? _configurationInstance;
        private static IPatientAggregateStore? _aggregateStoreInstance;
        public PatientsProjector(IConfiguration configuration, IPatientAggregateStore aggregateStore)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _aggregateStore = aggregateStore ?? throw new ArgumentNullException(nameof(aggregateStore));

            _cosmosClient = BuildCosmosClient(_configuration);

            SetConfigurationInstance(_configuration);
            SetAggrageteStoreInstance(_aggregateStore);
        }

        private static void SetConfigurationInstance(IConfiguration obj)
        {
            _configurationInstance = obj ?? throw new ArgumentNullException(nameof(obj));
        }

        private static void SetAggrageteStoreInstance(IPatientAggregateStore obj)
        {
            _aggregateStoreInstance = obj ?? throw new ArgumentNullException(nameof(obj));
        }

        private static CosmosClient BuildCosmosClient(IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration["CosmosDb:ConnectionString"]))
            {
                throw new ArgumentNullException("Missing 'ConnectionString' setting in configuration.");
            }

            return new CosmosClientBuilder(configuration["CosmosDb:ConnectionString"])
                .Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitializeContainersAsync(_cosmosClient, _configuration);
            _changeFeedProcessor = await StartChangeFeedProcessorAsync(_cosmosClient, _configuration);
        }

        private static async Task InitializeContainersAsync(CosmosClient cosmosClient,
                                                                IConfiguration configuration)
        {
            string databaseName = configuration["CosmosDb:DatabaseId"];
            string sourceContainerName = configuration["CosmosDb:ContainerId"];
            string leaseContainerName = configuration["CosmosDb:LeasesContainerId"];

            if (string.IsNullOrEmpty(databaseName)
                || string.IsNullOrEmpty(sourceContainerName)
                || string.IsNullOrEmpty(leaseContainerName))
            {
                throw new ArgumentNullException("'SourceDatabaseName', 'SourceContainerName', and 'LeasesContainerName' settings are required. Verify your configuration.");
            }

            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);

            await database.CreateContainerIfNotExistsAsync(new ContainerProperties(sourceContainerName, "/id"));

            await database.CreateContainerIfNotExistsAsync(new ContainerProperties(leaseContainerName, "/id"));
        }

        private static async Task<ChangeFeedProcessor> StartChangeFeedProcessorAsync(CosmosClient cosmosClient,
                                                                    IConfiguration configuration)
        {
            string databaseName = configuration["CosmosDb:DatabaseId"];
            string sourceContainerName = configuration["CosmosDb:ContainerId"];
            string leaseContainerName = configuration["CosmosDb:LeasesContainerId"];

            Container leaseContainer = cosmosClient.GetContainer(databaseName, leaseContainerName);
            ChangeFeedProcessor changeFeedProcessor = cosmosClient.GetContainer(databaseName, sourceContainerName)
                .GetChangeFeedProcessorBuilder<EventData>(processorName: "changeFeedPatient", onChangesDelegate: HandleChangesAsync)
                    .WithInstanceName("consoleHost")
                    .WithLeaseContainer(leaseContainer)
                    .Build();

            Console.WriteLine("Starting Change Feed Processor...");
            await changeFeedProcessor.StartAsync();
            Console.WriteLine("Change Feed Processor started.");

            return changeFeedProcessor;
        }

        private static async Task HandleChangesAsync(ChangeFeedProcessorContext context,
                                                        IReadOnlyCollection<EventData> changes,
                                                        CancellationToken cancellationToken)
        {
            Console.WriteLine("Handling changes.");
            using var conn = new SqlConnection(_configurationInstance.GetConnectionString("Hospital"));
            conn.EnsurePatientsTable();

            foreach (EventData item in changes)
            {
                var patientId = Guid.Parse(item.AggregateId.Replace("Patient-", string.Empty));
                Console.WriteLine($"Entity Patient {patientId} rehydrate");
                var patient = await _aggregateStoreInstance!.LoadAsync(PatientId.Create(patientId));
                conn.InsertPatient(patient);
            }

            conn.Close();
            Console.WriteLine("Finished handling changes.");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _changeFeedProcessor!.StopAsync();
        }
    }
}
