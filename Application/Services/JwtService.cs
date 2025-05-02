using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts;
using Application.Dtos.Request;
using Application.Services;
using Azure.Core;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public class JwtService : IJwtService
    {
        public string GerarToken(LoginRequest request)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            var key = Encoding.ASCII.GetBytes(jwtKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, request.Email),
                    new Claim(ClaimTypes.Role, "Administrador")
                }),

                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
