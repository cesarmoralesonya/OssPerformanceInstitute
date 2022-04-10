using Microsoft.AspNetCore.Mvc;
using OssPerformanceInstitute.AcademyContext.Api.Application;
using OssPerformanceInstitute.AcademyContext.Api.Commands;
using System.Reflection;

namespace OssPerformanceInstitute.AcademyContext.Api.Controllers.V1
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
    }
}
