using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praizer_api.Contracts.Requests;
using praizer_api.Services;

namespace praizer_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : ControllerBase
    {
        [HttpGet("getTeamMembers")]
        public async Task<IActionResult> GetTeamMembers(string uid)
        {
            try
            {
                var result = await CommonService.GetTeamMembers(uid);
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
