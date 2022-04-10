using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace OssPerformanceInstitute.SharedKernel.Common.Application
{
    public abstract class ApplicationServiceBase
    {
        private readonly ILogger _logger;
        protected ApplicationServiceBase(ILogger logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task PublishIntegrationEventAsync(IIntegrationEvent integrationEvent, string connectionString, string topicName)
        {

            try
            {
                var jsonMessage = JsonConvert.SerializeObject(integrationEvent);
                var body = Encoding.UTF8.GetBytes(jsonMessage);
                var client = new ServiceBusClient(connectionString);
                var sender = client.CreateSender(topicName);
                var message = new ServiceBusMessage()
                {
                    Body = new BinaryData(body),
                    MessageId = Guid.NewGuid().ToString(),
                    ContentType = MediaTypeNames.Application.Json,
                    Subject = integrationEvent.GetType().FullName
                };

                _logger?.LogInformation($"Sending message {message.MessageId}: body {jsonMessage}");
                await sender.SendMessageAsync(message);
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
            }
        }
    }
}
