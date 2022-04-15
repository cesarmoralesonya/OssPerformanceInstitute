using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OssPerformanceInstitute.HospitalBoundary.Infrastructure.HealthCkecks;

namespace OssPerformanceInstitute.HospitalBoundary.Infrastructure.Extensions
{
    public static class HealthChecksBuilderExtensions
    {
        public static IHealthChecksBuilder AddCosmosDbCheck(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.Add(new HealthCheckRegistration("OssPerformanceInstitute", new CosmosDbHealthChecks(configuration), HealthStatus.Unhealthy, null));
        }
    }
}
