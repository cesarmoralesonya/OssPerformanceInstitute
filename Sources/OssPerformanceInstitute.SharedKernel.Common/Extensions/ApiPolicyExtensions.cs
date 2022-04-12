using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using OssPerformanceInstitute.SharedKernel.Common.Options;

namespace OssPerformanceInstitute.SharedKernel.Common.Extensions
{
    public static class ApiPolicyExtensions
    {
        public static void AddApiPathPolicy(this IServiceCollection services)
        {
            services.AddRouting(setup => setup.LowercaseUrls = true);
        }

        public static void AddApiVersioningPolicy(this IServiceCollection services)
        {
            services.AddApiVersioning(setup =>
            {
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.ConfigureOptions<ConfigureSwaggerOptions>();
        }

        public static void AddSwaggerGenVersioningPolicy(this IServiceCollection services, string serviceName, string serviceDescription)
        {
            services.AddSwaggerGen(c =>
            {
                var swaggerVersions = c.SwaggerGeneratorOptions.SwaggerDocs;
                foreach (var swaggerVersion in swaggerVersions)
                {
                    swaggerVersion.Value.Title = serviceName;
                    swaggerVersion.Value.Description = serviceDescription;
                }
                c.SwaggerGeneratorOptions.SwaggerDocs = swaggerVersions;
            });
        }
        
        public static void UseSwaggerUIVersioningPolicy(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}
