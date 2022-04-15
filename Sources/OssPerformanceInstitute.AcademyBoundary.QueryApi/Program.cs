using OssPerformanceInstitute.SharedKernel.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);
const string SERVICENAME = "AcademyQuery.Api";
const string SERVICEDESCRIPTION = "Microservice of academy query in academy domain boundary";


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiPathPolicy();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioningPolicy();
builder.Services.AddSwaggerGenVersioningPolicy(SERVICENAME, SERVICEDESCRIPTION);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUIVersioningPolicy();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
