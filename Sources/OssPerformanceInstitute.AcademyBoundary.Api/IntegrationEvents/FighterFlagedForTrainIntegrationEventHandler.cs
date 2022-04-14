using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using OssPerformanceInstitute.AcademyBoundary.Api.Infrastructure;
using OssPerformanceInstitute.AcademyBoundary.Domain.Entities;
using OssPerformanceInstitute.AcademyBoundary.Domain.Repositories;
using OssPerformanceInstitute.AcademyBoundary.Domain.ValueObjects.FighterClientEntity;

namespace OssPerformanceInstitute.AcademyBoundary.Api.IntegrationEvents
{
    public class FighterFlagedForTrainIntegrationEventHandler : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<FighterFlagedForTrainIntegrationEventHandler> _logger;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusProcessor _serviceBusProcessor;

        public FighterFlagedForTrainIntegrationEventHandler(IConfiguration configuration,
                                                            IServiceScopeFactory serviceScopeFactory,
                                                            ILogger<FighterFlagedForTrainIntegrationEventHandler> logger)
        {
            this._serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceBusClient = new ServiceBusClient(configuration["ServiceBus:ConnectionString"]);
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(configuration["ServiceBus:TopicName"], configuration["ServiceBus:SubscriptionName"]);
            _serviceBusProcessor.ProcessMessageAsync += ProcessMessageAsync;
            _serviceBusProcessor.ProcessErrorAsync += ProcessErrorAsync;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _serviceBusProcessor.StartProcessingAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _serviceBusProcessor.StartProcessingAsync(cancellationToken);
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs messageEvent)
        {
            var body = messageEvent.Message.Body.ToString();
            var theEvent = JsonConvert.DeserializeObject<FighterFlaggedForTrainIntegrationEvent>(body);
            await messageEvent.CompleteMessageAsync(messageEvent.Message);
            _logger?.LogInformation($"message received: {body}");

            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AcademyDbContext>();
#nullable disable
            dbContext.FighterClientsMetadata.Add(theEvent);
#nullable disable
            var FighterClientRepository = scope.ServiceProvider.GetRequiredService<IFighterClientRepository>();
            var fighterClient = new FighterClient(FighterClientId.Create(theEvent.Id));
            await FighterClientRepository.AddAsync(fighterClient);
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs errorEvent)
        {
            _logger.LogError($"Error processing Message: {errorEvent.Exception}");
            return Task.CompletedTask;
        }
    }
}
