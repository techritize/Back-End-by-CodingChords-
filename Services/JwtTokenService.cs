using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechritizeAuthAPI.Helpers;
using TechritizeAuthAPI.Models;

namespace TechritizeAuthAPI.Services
{
    public class JwtTokenService
    {
        private readonly AppSettings _appSettings;

        public JwtTokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(User user)
        {
            // Claims = Info stored in token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("name", user.Name),
                new Claim("role", user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Create signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token setup
            var token = new JwtSecurityToken(
                issuer: _appSettings.Jwt.Issuer,
                audience: _appSettings.Jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_appSettings.Jwt.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
