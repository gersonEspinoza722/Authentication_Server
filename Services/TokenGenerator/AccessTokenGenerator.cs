using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Services.TokenGenerator
{
    public class AccessTokenGenerator
    {
        public string Generate(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5NzpXDs_I9OMj9QKc8ntoxnxZzkyGpTDyla0EvaI3Ky8ZtodJxnp0ReX11OtyYqL0-jdSqgqwXZWeLuD9Fd1hfXzjeholeD8g4f3oXE4cmccwGS3Vr15cA9vOsVu-yEctJQTAmh3XAIh3cVwono0Vgrxsgg3SBIjuUW-KfdsQE8"));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id",user.id.ToString()),
                new Claim(ClaimTypes.Email,user.email),
                new Claim(ClaimTypes.Name,user.username)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                "https://localhost:5001",
                "https://localhost:5001",
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(30),
                credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}