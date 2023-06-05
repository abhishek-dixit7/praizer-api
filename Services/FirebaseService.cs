using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;

namespace praizer_api.Services
{

    public class FirebaseService
    {
        private readonly IConfiguration configuration;

        public FirebaseService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(string uid, string email)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var jwtService = new JwtService(secretKey);

            return jwtService.GenerateToken(uid, email);
        }

        public async Task<UserRecord> GetUserAsync(string uid)
        {
            return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        }
    }


}
