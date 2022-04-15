using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace OssPerformanceInstitute.AcademyBoundary.QueryApi.Controllers.V1
{
    /// <summary>
    /// Application Segregation Query of CQRS Pattern
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class AcademyController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AcademyController> _logger;

        public AcademyController(IConfiguration configuration, ILogger<AcademyController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var query = @"SELECT TOP (1000) FCM.[name] AS fighterName
					                            ,FCM.[country] AS country
					                            ,FCM.[city] AS city
					                            ,FCM.[date_birth] AS dateOfBirth
					                            ,[sex] = 
					                            CASE FCM.[sex]
						                            WHEN 0 THEN 'Male'
						                            WHEN 1 THEN 'Famele'
					                            END
					                            ,T.[name] AS trainerName
					                            ,T.[muay_thai] AS isMuayThaiTrainer
					                            ,T.[bjj] AS isBjjTrainner
					                            ,T.[boxing] AS isBoxingTrainner
					                            ,T.[kick_boxing] AS isKickBoxingTrainner
					                            ,T.[mma] AS isMmaTrainner
                            FROM [Academy].[dbo].[t_fighter_client_metadata] AS FCM
                            JOIN [Academy].[dbo].[t_fighter_client] AS FC
                            ON FCM.[fighter_client_id] = FC.[pk_fighter_client_id]
                            LEFT JOIN [Academy].[dbo].[t_trainer] AS T
                            ON FC.[trainer_id] = T.[pk_trainer_id]";

                using var connection = new SqlConnection(_configuration.GetConnectionString("Academy"));
                var academyData = (await connection.QueryAsync(query)).ToList();

                return Ok(academyData);
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
