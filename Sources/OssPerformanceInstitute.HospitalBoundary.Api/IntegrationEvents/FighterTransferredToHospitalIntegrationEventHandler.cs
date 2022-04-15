using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using OssPerformanceInstitute.HospitalBoundary.Api.Infrastructure;
using OssPerformanceInstitute.HospitalBoundary.Domain.Entities;
using OssPerformanceInstitute.HospitalBoundary.Domain.Repositories;
using OssPerformanceInstitute.HospitalBoundary.Domain.ValueObjects;

namespace OssPerformanceInstitute.HospitalBoundary.Api.IntegrationEvents
{
    public class FighterTransferredToHospitalIntegrationEventHandler : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IPatientAggregateStore _patientAggregateStore;
        private readonly ILogger<FighterTransferredToHospitalIntegrationEventHandler> _logger;
        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;
        public FighterTransferredToHospitalIntegrationEventHandler(IConfiguration configuration,
                                                               IServiceScopeFactory serviceScopeFactory,
                                                               IPatientAggregateStore patientAggregateStore,
                                                               ILogger<FighterTransferredToHospitalIntegrationEventHandler> logger)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this._patientAggregateStore = patientAggregateStore;
            this._logger = logger;

            _client = new ServiceBusClient(configuration["ServiceBus:ConnectionString"]);
            _processor = _client.CreateProcessor(configuration["ServiceBus:TopicName"], configuration["ServiceBus:SubscriptionName"]);
            _processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
            _processor.ProcessErrorAsync += Processor_ProcessErrorAsync;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _processor.StartProcessingAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.StopProcessingAsync(cancellationToken);
        }

        private async Task Processor_ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var body = args.Message.Body.ToString();
            var theEvent = JsonConvert.DeserializeObject<FighterTransferredToHospitalIntegrationEvent>(body);

            await args.CompleteMessageAsync(args.Message);

            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();

            var existingPatient = await dbContext.PatientsMetadata.FindAsync(theEvent?.Id);


            if (existingPatient == null)
            {
                dbContext.PatientsMetadata.Add(theEvent!);
                await dbContext.SaveChangesAsync();
            }

            var patientId = PatientId.Create(theEvent!.Id);

            var patient = new Patient(patientId);
            await _patientAggregateStore.SaveAsync(patient);
        }

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}