using Asp.Versioning;
using Diaverum.Domain;
using Diaverum.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Diaverum.API.Controller
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class LabResultController(
        ILogger<DiaverumItemController> logger,
        ILabResultService labResultService) : ControllerBase
    {
        [HttpPost("")]
        [Consumes(MediaTypeNames.Text.Plain)]
        public async Task<ActionResult> SubmitLabResultAsync([FromBody] string labResult)
        {
            logger.LogDebug("{message}", $"Call to " +
                $"{nameof(ControllerContext.ActionDescriptor.ActionName)}");

            await labResultService.SubmitLabSersult(labResult);
            return Ok();
        }

        [HttpGet("")]
        public async Task<ActionResult<LabResultDTO>> GetLabResultAsync([FromQuery] string? clinicNo = null, [FromQuery] int? patientId = null)
        {
            logger.LogDebug("{message}", $"Call to " +
                $"{nameof(ControllerContext.ActionDescriptor.ActionName)} with " +
                $"{nameof(clinicNo)} = '{clinicNo}', " +
                $"{nameof(patientId)} = '{patientId}'");

            return Ok(await labResultService.GetLabResult(clinicNo, patientId));
        }
    }
}
