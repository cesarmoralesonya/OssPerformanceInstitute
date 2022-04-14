using Microsoft.AspNetCore.Mvc;
using OssPerformanceInstitute.AcademyBoundary.Api.Application;
using OssPerformanceInstitute.AcademyBoundary.Api.Commands;
using System.Reflection;

namespace OssPerformanceInstitute.AcademyBoundary.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class AcademyController : ControllerBase
    {
        private readonly AcademyApplicationService _academyApplicationService;
        private readonly ILogger<AcademyController> _logger;

        public AcademyController(AcademyApplicationService cademyApplicationService, ILogger<AcademyController> logger)
        {
            _academyApplicationService = cademyApplicationService ?? throw new ArgumentNullException(nameof(cademyApplicationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateTrainerCommand command)
        {

            try
            {
                var result = await _academyApplicationService.HandleCommandAsync(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("requesttraining")]
        public async Task<IActionResult> Post(RequestTrainingCommand command)
        {
            try
            {
                if (command == null)
                    return BadRequest("Command can not be null");

                await _academyApplicationService.HandleCommandAsync(command);

                return Ok();

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
