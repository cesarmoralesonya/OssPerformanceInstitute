using OssPerformanceInstitute.HospitalBoundary.Domain.Repositories;
using OssPerformanceInstitute.HospitalBoundary.Infrastructure.Extensions;
using OssPerformanceInstitute.HospitalBoundary.Infrastructure.Repositories;
using OssPerformanceInstitute.HospitalBoundary.Projector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IPatientAggregateStore, PatientAggregateStore>();
builder.Services.AddHostedService<PatientsProjector>();
builder.Services.AddHealthChecks()
                    .AddCosmosDbCheck(builder.Configuration);

var app = builder.Build();
app.MapHealthChecks("/health");
app.Run();