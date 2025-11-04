using Asp.Versioning;
using Diaverum.Service;
using Diaverum.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Diaverum.API.Controller
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class DiaverumItemController(
        ILogger<DiaverumItemController> logger,
        IDiaverumItemService diaverumItemService) : ControllerBase
    {
        [HttpPost("")]
        public async Task<ActionResult<DiaverumItemDTO>> AddDiaverumItemAsync([FromBody] DiaverumItemDTO diaverumItemDto)
        {
            logger.LogDebug("{message}", @$"Call to 
                {nameof(ControllerContext.ActionDescriptor.ActionName)}");

            return Ok(await diaverumItemService.AddDiaverumItemAsync(diaverumItemDto));
        }

        [HttpGet("{diaverumItemId}")]
        public async Task<ActionResult<DiaverumItemDTO>> GetDiaverumItemAsync(short diaverumItemId)
        {
            logger.LogDebug("{message}", @$"Call to 
                {nameof(ControllerContext.ActionDescriptor.ActionName)}, 
                {diaverumItemId}");

            return Ok(await diaverumItemService.GetDiaverumItemAsync(diaverumItemId));
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<DiaverumItemDTO>>> GetDiaverumItemListAsync()
        {
            logger.LogDebug("{message}", @$"Call to 
                {nameof(ControllerContext.ActionDescriptor.ActionName)}");

            return Ok(await diaverumItemService.GetDiaverumItemListAsync());
        }

        [HttpPut("")]
        public async Task<ActionResult<DiaverumItemDTO>> UpdateDiaverumItemAsync([FromBody] DiaverumItemDTO diaverumItemDTO)
        {
            logger.LogDebug("{message}", @$"Call to 
                {nameof(ControllerContext.ActionDescriptor.ActionName)}");
                        
            return Ok(await diaverumItemService.UpdateDiaverumItemAsync(diaverumItemDTO));
        }

        [HttpDelete("{diaverumItemId}")]
        public async Task<ActionResult<DiaverumItemDTO>> DeleteDiaverumItemAsync(short diaverumItemId)
        {
            logger.LogDebug("{message}", @$"Call to 
                {nameof(ControllerContext.ActionDescriptor.ActionName)}, 
                {diaverumItemId}");

            await diaverumItemService.DeleteDiaverumItemAsync(diaverumItemId);
            return Ok();
        }
    }
}
