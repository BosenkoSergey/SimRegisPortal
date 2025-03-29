using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimRegisPortal.Application.Constants;
using SimRegisPortal.Application.Models;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.AppSettings;
using SimRegisPortal.Core.AppSettings.Components;

namespace SimRegisPortal.Application.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly AccessTokenSettings _settings;

        public AccessTokenService(IOptions<AppSettings> options)
        {
            _settings = options.Value.AuthSettings.AccessToken;
        }

        public string GenerateToken(UserTokenModel user)
        {
            var claims = new List<Claim>
            {
                new (CustomClaimTypes.UserAccountId, user.UserId.ToString()),
                new (CustomClaimTypes.UserSessionId, user.SessionId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
