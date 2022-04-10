using Microsoft.EntityFrameworkCore;
using OssPerformanceInstitute.FighterContext.Api.Infrastructure;

namespace OssPerformanceInstitute.FighterContext.Api.Extensions
{
    public static class FighterDbContextExtensions
    {
        public static void AddFighterDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FighterDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Fighter"));
            });
        }
        public static void EnsureFighterDbIsCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<FighterDbContext>();
            context.Database.EnsureCreated();
            context.Database.CloseConnection();
        }
    }
}
