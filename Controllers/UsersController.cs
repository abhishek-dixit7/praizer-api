﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praizer_api.Contracts.Requests;
using praizer_api.Services;

namespace praizer_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("getUserDetails")]

        public async Task<IActionResult> GetUserDeatils()
        {
            try
            {
                var result = await UserService.GetUserDetails();
                return Ok(result);
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e);
                return NotFound(e);
            }
        }

        [HttpGet("getUserDetailsByUid")]

        public async Task<IActionResult> GetUserDeatilsByUid(string uid)
        {
            try
            {
                var result = await UserService.GetUserDetailsByUid(uid);
                return Ok(result);
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e);
                return NotFound(e);
            }

        }
        
        [HttpGet("getUserDetailsByName")]

        public async Task<IActionResult> GetUserDeatilsByName(string name)
        {
            try
            {
                var result = await UserService.GetUserDeatilsByName(name);
                return Ok(result);
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e);
                return NotFound(e);
            }
        }

        [HttpPost("updateUserDetailsByUid")]
        public async Task<IActionResult> UpdateUserDetailsByUid([FromForm] UpdateUserRequest request)
        {
            try
            {
                var result = await UserService.UpdateUserDetailsByUid(request);
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
