using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praizer_api.Database.Models;
using praizer_api.Responses;
using praizer_api.Services;

namespace praizer_api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class Users : ControllerBase
    {
        [HttpGet("api/[controller]/getUserDetails")]

        public async Task<List<UserResponse>> GetUserDeatils()
        {
            return await UserService.GetUserDetails();
        }
    }
}
