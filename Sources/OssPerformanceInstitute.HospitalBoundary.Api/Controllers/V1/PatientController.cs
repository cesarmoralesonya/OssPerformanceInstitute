using Microsoft.AspNetCore.Mvc;
using OssPerformanceInstitute.HospitalBoundary.Api.ApplicationServices;
using OssPerformanceInstitute.HospitalBoundary.Api.Commands;
using System.Reflection;

namespace OssPerformanceInstitute.HospitalBoundary.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class PatientController : ControllerBase
    {
        private readonly HospitalApplicationService _applicationService;
        private readonly ILogger<PatientController> _logger;

        public PatientController(HospitalApplicationService applicationService,
                                 ILogger<PatientController> logger)
        {
            this._applicationService = applicationService;
            this._logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpPut("weight")]
        public async Task<IActionResult> Put(SetWeightCommand command)
        {
            try
            {
                await _applicationService.HandleAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
                return BadRequest(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPut("bloodType")]
        public async Task<IActionResult> Put(SetBloodTypeCommand command)
        {
            try
            {
                await _applicationService.HandleAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
                return BadRequest(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("admit")]
        public async Task<IActionResult> Post(AdmitPatientCommand command)
        {
            try
            {
                await _applicationService.HandleAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
                return BadRequest(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("discharge")]
        public async Task<IActionResult> Post(DischargePatientCommand command)
        {
            try
            {
                await _applicationService.HandleAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
                return BadRequest(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost("procedure")]
        public async Task<IActionResult> Post(AddProcedureCommand command)
        {
            try
            {
                await _applicationService.HandleAsync(command);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorMessage = $"{GetType().Name} {MethodBase.GetCurrentMethod()?.Name} Ex: {(ex.InnerException ?? ex).Message}";
                _logger.LogError(errorMessage);
                return BadRequest(ex.Message);
            }
        }
    }
}
