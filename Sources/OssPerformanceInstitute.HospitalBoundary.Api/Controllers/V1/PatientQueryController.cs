using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace OssPerformanceInstitute.HospitalBoundary.Api.Controllers.V1
{
    /// <summary>
    /// Application Segregation Query of CQRS Pattern
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PatientQueryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PatientQueryController> _logger;

        public PatientQueryController(IConfiguration configuration, ILogger<PatientQueryController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                string query = @"SELECT TOP (1000) PM.[name] 
	                                              ,P.[blood_type] AS bloodType
                                                  ,P.[weight]
                                                  ,P.[status]
	                                              ,[sex] = 
					                                CASE PM.[sex]
						                                WHEN 0 THEN 'Male'
						                                WHEN 1 THEN 'Famele'
					                                END
	                                              ,PM.[date_birth] AS dateOfBirth
                                                  ,P.[updated_on]
                                              FROM [Hospital].[dbo].[t_patient] AS P
                                              JOIN [Hospital].[dbo].[t_patient_metadata] AS PM
                                              ON P.[patient_id] = PM.[patient_id]";

                using var connection = new SqlConnection(_configuration.GetConnectionString("Hospital"));
                var patients = (await connection.QueryAsync(query)).ToList();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);

                return BadRequest(errorMessage);
            }
        }
    }

}
