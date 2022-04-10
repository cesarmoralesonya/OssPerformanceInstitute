using Microsoft.Extensions.DependencyInjection;
using OssPerformanceInstitute.SharedKernel.Domain.Repositories;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Repositories;

namespace OssPerformanceInstitute.SharedKernel.Infrastructure.Extensions
{
    public static class EfRepositoryExtensions
    {
        public static void AddEfRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
        }
    }
}
