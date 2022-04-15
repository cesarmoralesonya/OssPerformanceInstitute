using OssPerformanceInstitute.FighterBoundary.Api.Application;
using OssPerformanceInstitute.FighterBoundary.Api.Infrastructure;
using OssPerformanceInstitute.FighterBoundary.Domain.Repositories;
using OssPerformanceInstitute.FighterBoundary.Domain.Services;
using OssPerformanceInstitute.SharedKernel.Common.Extensions;
using OssPerformanceInstitute.SharedKernel.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
const string SERVICENAME = "Fighter.Api";
const string SERVICEDESCRIPTION = "Microservice of fighter domain boundary";


// Add services to the container.
builder.Services.AddDbSqlServerByConnectionString<FighterDbContext>(builder.Configuration, "Fighter");
builder.Services.AddEfRepository();
builder.Services.AddScoped<IFighterRepository, FighterRepository>();
builder.Services.AddScoped<ICitizenshipService, CitizenshipService>();
builder.Services.AddScoped<FighterApplicationService>();

builder.Services.AddControllers();

builder.Services.AddApiPathPolicy();
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
