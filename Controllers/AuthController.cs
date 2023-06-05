using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using praizer_api.Services;

namespace praizer_api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly FirebaseService firebaseService;

        public AuthController(FirebaseService firebaseService)
        {
            this.firebaseService = firebaseService;
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
                var userRecord = await firebaseService.GetUserAsync(uid);

                // Generate a JWT token
                var token = await firebaseService.GenerateTokenAsync(userRecord.Uid, userRecord.Email);

                // Return the token to the client
                return Ok(new { Token = token });
            }
            catch (FirebaseException ex)
            {
                // Handle Firebase verification errors
                return BadRequest(ex.Message);
            }
        }
    }

    public class FirebaseTokenRequest
    {
        public string IdToken { get; set; }
    }


}