using OssPerformanceInstitute.AcademyBoundary.Api.Application;
using OssPerformanceInstitute.AcademyBoundary.Api.Infrastructure;
using OssPerformanceInstitute.AcademyBoundary.Api.IntegrationEvents;
using OssPerformanceInstitute.AcademyBoundary.Domain.Repositories;
using OssPerformanceInstitute.SharedKernel.Common.Extensions;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
const string SERVICENAME = "Academy.Api";
const string SERVICEDESCRIPTION = "Microservice of academy domain boundary";

// Add services to the container.
builder.Services.AddDbSqlServerByConnectionString<AcademyDbContext>(builder.Configuration, "Academy");
builder.Services.AddEfRepository();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<IFighterClientRepository, FighterClientRepository>();
builder.Services.AddHostedService<FighterFlaggedForTrainIntegrationEventHandler>();
builder.Services.AddScoped<TrainerApplicationService>();

builder.Services.AddControllers();

builder.Services.AddApiPathPolicy();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioningPolicy();
builder.Services.AddSwaggerGenVersioningPolicy(SERVICENAME, SERVICEDESCRIPTION);

var app = builder.Build();

app.EnsureDbIsCreated<AcademyDbContext>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUIVersioningPolicy();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
