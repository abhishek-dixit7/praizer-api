using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using praizer_api.Contracts.Requests;
using praizer_api.Services;

namespace praizer_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LogInController : ControllerBase
    {
       
        private readonly FirebaseService _firebaseService;

        public LogInController(FirebaseService firebaseService)
        {
            this._firebaseService = firebaseService;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignupRequest request)
        {
            try
            {
                var result = await new LoginService(_firebaseService).SignUp(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                return NotFound(e);
            }
        }

        [HttpGet("LoginWithUsername")]
        public async Task<IActionResult> LoginWithUsername(LoginRequest request)
        {
            try
            {
                var result = await new LoginService(_firebaseService).LoginWithUsername(request);
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
