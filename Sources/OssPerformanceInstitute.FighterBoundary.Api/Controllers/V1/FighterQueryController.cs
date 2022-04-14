using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace OssPerformanceInstitute.FighterBoundary.Api.Controllers.V1
{
    /// <summary>
    /// Application Segregation Query of CQRS Pattern
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FighterQueryController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FighterQueryController> _logger;

        public FighterQueryController(IConfiguration configuration, ILogger<FighterQueryController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                string query = @"SELECT TOP (1000) F.[pk_fighter_id] AS fighterid
                                                  ,F.[name] 
                                                  ,F.[country]
                                                  ,F.[city]
                                                  ,[sex] = 
	                                              CASE F.[sex]
		                                            WHEN 0 THEN 'Male'
		                                            WHEN 1 THEN 'Famele'
	                                              END
                                                  ,F.[date_birth] AS datebirth
                                              FROM [Fighter].[dbo].[t_fighter] AS F";

                using var connection = new SqlConnection(_configuration.GetConnectionString("Fighter"));
                var fighters = (await connection.QueryAsync(query)).ToList();
                return Ok(fighters);
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
