using Microsoft.OpenApi.Models;
using MMLib.Ocelot.Provider.AppConfiguration;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddOcelotWithSwaggerSupport((o) =>
{
    o.Folder = "Configuration";
});
builder.Services.AddOcelot()
                .AddAppConfiguration();

builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "OssPerformanceInstitute API",
    Version = "v1"
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();
app.UseSwagger();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseStaticFiles();

app.UseSwaggerForOcelotUI(opt => {
    opt.PathToSwaggerGenerator = "/swagger/docs";
}).UseOcelot();

app.Run();