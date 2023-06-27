using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praizer_api.Contracts.Responses;
using praizer_api.Objects;
using praizer_api.Services;

namespace praizer_api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly FirebaseService _firebaseService;

        public AuthController(FirebaseService firebaseService)
        {
            this._firebaseService = firebaseService;
        }

        [HttpPost("firebase-login")]
        public async Task<IActionResult> FirebaseLogin([FromBody] FirebaseTokenRequest tokenRequest)
        {
            try
            {
                var idToken = tokenRequest.IdToken;

                // Verify the Firebase ID token
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);

                // Get the user's unique ID from the decoded token
                var uid = decodedToken.Uid;

                // Get the user record from Firebase
                var userRecord = await _firebaseService.GetUserAsync(uid);
                
                // Store user details in DB if not already exists
                await AuthService.AddUpdateUser(userRecord);

                // Generate a JWT token
                var token = await _firebaseService.GenerateTokenAsync(userRecord.Uid, userRecord.Email);

                // Return the token to the client
                return Ok(new LoginResponse{ Token = token,CurrentUserId=uid });
            }
            catch (FirebaseException ex)
            {
                // Handle Firebase verification errors
                return BadRequest(ex.Message);
            }
        }
    }

    
    


}