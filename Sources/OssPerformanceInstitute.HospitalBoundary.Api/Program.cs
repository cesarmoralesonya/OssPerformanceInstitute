using OssPerformanceInstitute.HospitalBoundary.Api.ApplicationServices;
using OssPerformanceInstitute.HospitalBoundary.Api.Infrastructure;
using OssPerformanceInstitute.HospitalBoundary.Api.IntegrationEvents;
using OssPerformanceInstitute.HospitalBoundary.Domain.Repositories;
using OssPerformanceInstitute.HospitalBoundary.Infrastructure.Extensions;
using OssPerformanceInstitute.HospitalBoundary.Infrastructure.Repositories;
using OssPerformanceInstitute.SharedKernel.Common.Extensions;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
const string SERVICENAME = "Hospital.Api";
const string SERVICEDESCRIPTION = "Microservice of Hospital domain boundary";

// Add services to the container.

builder.Services.AddDbSqlServerByConnectionString<HospitalDbContext>(builder.Configuration, "Hospital");

builder.Services.AddHealthChecks()
                    .AddCosmosDbCheck(builder.Configuration);
builder.Services.AddSingleton<IPatientAggregateStore, PatientAggregateStore>();

builder.Services.AddScoped<HospitalApplicationService>();
builder.Services.AddHostedService<FighterTransferredToHospitalIntegrationEventHandler>();

builder.Services.AddControllers();

builder.Services.AddApiPathPolicy();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioningPolicy();
builder.Services.AddSwaggerGenVersioningPolicy(SERVICENAME, SERVICEDESCRIPTION);

var app = builder.Build();

app.EnsureDbIsCreated<HospitalDbContext>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUIVersioningPolicy();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
