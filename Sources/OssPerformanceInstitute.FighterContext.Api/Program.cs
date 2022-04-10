using OssPerformanceInstitute.FighterContext.Api.Application;
using OssPerformanceInstitute.FighterContext.Api.Extensions;
using OssPerformanceInstitute.FighterContext.Api.Infrastructure;
using OssPerformanceInstitute.FighterContext.Domain.Repositories;
using OssPerformanceInstitute.FighterContext.Domain.Services;
using OssPerformanceInstitute.SharedKernel.Common.Extensions;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
const string SERVICENAME = "Fighter.Api";
const string SERVICEDESCRIPTION = "Microservice of fighter domain context";


// Add services to the container.

builder.Services.AddFighterDb(builder.Configuration);
builder.Services.AddEfRepository();
builder.Services.AddScoped<IFighterRepository, FighterRepository>();
builder.Services.AddScoped<ICitizenshipService, CitizenshipService>();
builder.Services.AddScoped<FighterApplicationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioningPolicy();
builder.Services.AddSwaggerGen(c =>
{
    var swaggerVersions = c.SwaggerGeneratorOptions.SwaggerDocs;
    foreach(var swaggerVersion in swaggerVersions)
    {
        swaggerVersion.Value.Title = SERVICENAME;
        swaggerVersion.Value.Description = SERVICEDESCRIPTION;
    }
    c.SwaggerGeneratorOptions.SwaggerDocs = swaggerVersions;
});

var app = builder.Build();


app.EnsureFighterDbIsCreated();
// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseApiVersioningSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
