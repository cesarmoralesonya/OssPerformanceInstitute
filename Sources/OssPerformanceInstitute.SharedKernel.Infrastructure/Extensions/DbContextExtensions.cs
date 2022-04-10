using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OssPerformanceInstitute.SharedKernel.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static void AddDbSqlServerByConnectionString<TContext>(this IServiceCollection services, IConfiguration configuration, string connectionStringName) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(connectionStringName));
            });
        }
        public static void EnsureDbIsCreated<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<TContext>();
            context?.Database.EnsureCreated();
            context?.Database.CloseConnection();
        }
    }
}
