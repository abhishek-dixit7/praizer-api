using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praizer_api.Contracts.Requests;
using praizer_api.Services;


namespace praizer_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PraisesController : ControllerBase
    {
        [HttpGet("GetPraises")]
        public async Task<IActionResult> GetPraises()
        {
            try
            {
                var result = await PraisesService.GetPraises();
                return Ok(result);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return NotFound(e);
            }
        }

        [HttpPost("CreatePraises")]
        public async Task<IActionResult> CreatePraise(CreatePraiseRequest request)
        {
            try
            {

                var result = await PraisesService.CreatePraise(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return NotFound(e);
            }
        }


    }
}
