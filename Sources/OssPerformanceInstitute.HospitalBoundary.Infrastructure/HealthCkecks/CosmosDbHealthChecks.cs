using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OssPerformanceInstitute.HospitalBoundary.Infrastructure.HealthCkecks
{
    internal class CosmosDbHealthChecks : IHealthCheck
    {
        private readonly CosmosClient _cosmosClient;
        private readonly IConfiguration _configuration;

        public CosmosDbHealthChecks(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._cosmosClient = new CosmosClient(configuration["CosmosDb:ConnectionString"]);
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            await _cosmosClient.ReadAccountAsync();
            var databaseId = _configuration["CosmosDb:DatabaseId"];
            var containerId = _configuration["CosmosDb:ContainerId"];
            var container = _cosmosClient.GetContainer(databaseId, containerId);
            var containerProperties = await container.ReadContainerAsync(cancellationToken: cancellationToken);
            return containerProperties.StatusCode == System.Net.HttpStatusCode.OK ? HealthCheckResult.Healthy() :
                HealthCheckResult.Unhealthy();
        }
    }
}
