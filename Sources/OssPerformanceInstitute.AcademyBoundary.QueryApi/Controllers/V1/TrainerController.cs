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
    public class TrainerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TrainerController> _logger;

        public TrainerController(IConfiguration configuration, ILogger<TrainerController> logger)
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
                var query = @"SELECT TOP (1000) T.[pk_trainer_id] AS trainerId
                                              ,T.[name]
                                              ,T.[muay_thai] AS isMuayThaiTrainer
                                              ,T.[bjj] AS isBjjTrainner
                                              ,T.[boxing] AS isBoxingTrainner
                                              ,T.[kick_boxing] AS isKickBoxingTrainner
                                              ,T.[mma] AS isMmaTrainner
                                          FROM [Academy].[dbo].[t_trainer] AS T";

                using var connection = new SqlConnection(_configuration.GetConnectionString("Academy"));
                var trainers = (await connection.QueryAsync(query)).ToList();

                return Ok(trainers);
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
