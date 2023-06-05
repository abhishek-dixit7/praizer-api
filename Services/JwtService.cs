using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace praizer_api.Services
{
    public class JwtService
    {
        private readonly string secretKey;

        public JwtService(string secretKey)
        {
            this.secretKey = secretKey;
  
        }

        public string GenerateToken(string userId, string username)
        {
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var _configurations = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var jwtSettings = _configurations.GetSection("JwtSettings");
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],             
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
