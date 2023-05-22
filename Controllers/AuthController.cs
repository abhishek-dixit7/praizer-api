using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using praizer_api.Objects;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace praizer_api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUri = "https://localhost:7226/api/auth/callback";
            var clientId = _configuration.GetRequiredSection("GoogleAuth")["ClientId"];

            var parameters = new Dictionary<string, string>
                {
                    { "client_id", clientId! },
                    { "response_type", "code" },
                    { "redirect_uri", redirectUri },
                    { "scope", "openid email profile" } // Customize the required scopes as per your needs
                };

            var authorizationUrl = QueryHelpers.AddQueryString("https://accounts.google.com/o/oauth2/v2/auth", parameters);

            return Redirect(authorizationUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> GoogleResponse([FromQuery] string code)
        {
            var googleAuthUrl = "https://www.googleapis.com/oauth2/v4/token";
            var redirectUri = "https://localhost:7226/api/auth/callback";
            var clientId = _configuration.GetRequiredSection("GoogleAuth")["ClientId"];
            var clientSecret = _configuration.GetRequiredSection("GoogleAuth")["ClientSecret"];

            var tokenRequestParameters = new Dictionary<string, string>
                {
                    { "code", code },
                    { "client_id", clientId! },
                    { "client_secret", clientSecret! },
                    { "redirect_uri", redirectUri },
                    { "grant_type", "authorization_code" }
                };

            // Exchange the authorization code with an access token
            using (var client = new HttpClient())
            {
                var tokenResponse = await client.PostAsync(googleAuthUrl, new FormUrlEncodedContent(tokenRequestParameters));
                var tokenContent = await tokenResponse.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<GoogleTokenResponse>(tokenContent);

                // Get user details from the userinfo endpoint
                var userinfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                var userinfoResponse = await client.GetAsync(userinfoUrl);
                var userinfoContent = await userinfoResponse.Content.ReadAsStringAsync();
                var userinfo = JsonConvert.DeserializeObject<GoogleUserInfo>(userinfoContent);

                // Extract relevant user details
                var userId = userinfo.Id;
                var email = userinfo.Email;
                var name = userinfo.Name;
                // ...

                // Generate a JWT token
                var jwtToken = GenerateJwtToken(token.AccessToken, userId, email, name);

                // Optionally, save the user details to your user database table

                return Ok(new { access_token = jwtToken });
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            // Clear the JWT token from the client-side storage
            // For example, if using local storage:
            // localStorage.removeItem('token');

            // Perform the sign-out from the Google OAuth provider
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

            // Return a response indicating successful logout
            return Ok(new { message = "Logout successful" });
        }

        private string GenerateJwtToken(string accessToken, string userId, string email, string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetRequiredSection("Jwt")["IssuerSigningKey"]!); // Replace with your secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("access_token", accessToken),
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, name)
                    // Add additional claims as needed
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Set token expiration as per your requirement
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }

}