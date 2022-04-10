using OssPerformanceInstitute.FighterContext.Api.Application;
using OssPerformanceInstitute.FighterContext.Api.Infrastructure;
using OssPerformanceInstitute.FighterContext.Domain.Repositories;
using OssPerformanceInstitute.FighterContext.Domain.Services;
using OssPerformanceInstitute.SharedKernel.Common.Extensions;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
const string SERVICENAME = "Fighter.Api";
const string SERVICEDESCRIPTION = "Microservice of fighter domain context";


// Add services to the container.
builder.Services.AddDbSqlServerByConnectionString<FighterDbContext>(builder.Configuration, "Fighter");
builder.Services.AddEfRepository();
builder.Services.AddScoped<IFighterRepository, FighterRepository>();
builder.Services.AddScoped<ICitizenshipService, CitizenshipService>();
builder.Services.AddScoped<FighterApplicationService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioningPolicy();
builder.Services.AddSwaggerGenVersioningPolicy(SERVICENAME, SERVICEDESCRIPTION);

var app = builder.Build();


app.EnsureDbIsCreated<FighterDbContext>();
// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUIVersioningPolicy();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
