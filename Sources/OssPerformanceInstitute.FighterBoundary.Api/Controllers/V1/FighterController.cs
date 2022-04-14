using Microsoft.AspNetCore.Mvc;
using OssPerformanceInstitute.FighterBoundary.Api.Application;
using OssPerformanceInstitute.FighterBoundary.Api.Commands;
using System.Reflection;

namespace OssPerformanceInstitute.FighterBoundary.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FighterController : ControllerBase
    {
        private readonly FighterApplicationService _fighterApplicationService;
        private readonly ILogger<FighterController> _logger;

        public FighterController(FighterApplicationService fighterApplicationService, ILogger<FighterController> logger)
        {
            this._fighterApplicationService = fighterApplicationService ?? throw new ArgumentNullException(nameof(fighterApplicationService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [MapToApiVersion("1.0")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateFighterCommand command)
        {
            try
            {
                var result = await _fighterApplicationService.HandleCommandAsync(command);
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
        [HttpPost("flagfortrain")]
        public async Task<IActionResult> Post(FlagForTrainCommand command)
        {
            try
            {
                await _fighterApplicationService.HandleCommandAsync(command);
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
