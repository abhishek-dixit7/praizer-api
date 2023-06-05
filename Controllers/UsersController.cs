using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praizer_api.Database.Models;
using praizer_api.Responses;
using praizer_api.Services;

namespace praizer_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("getUserDetails")]

        public async Task<List<UserResponse>> GetUserDeatils()
        {
            return await UserService.GetUserDetails();
        }
    }
}
