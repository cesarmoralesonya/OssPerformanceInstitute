using Microsoft.AspNetCore.Mvc;
using OssPerformanceInstitute.FighterContext.Api.Application;
using OssPerformanceInstitute.FighterContext.Api.Commands;
using System.Reflection;

namespace OssPerformanceInstitute.FighterContext.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FighterController :ControllerBase
    {
        private readonly FighterApplicationService _fighterApplicationService;
        private readonly ILogger<FighterController> _logger;

        public FighterController(FighterApplicationService fighterApplicationService, ILogger<FighterController> logger)
        {
            this._fighterApplicationService = fighterApplicationService ?? throw new ArgumentNullException(nameof(fighterApplicationService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [MapToApiVersion("2.0")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateFighterCommand command)
        {
            try
            {
                await _fighterApplicationService.HandleCommandAsync(command);
                return Ok(command);
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod().Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }
        }
    }
}
