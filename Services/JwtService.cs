﻿using Microsoft.IdentityModel.Tokens;
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
            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",             
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}