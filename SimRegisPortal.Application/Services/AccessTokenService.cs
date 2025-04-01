using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimRegisPortal.Application.Constants;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.AppSettings;
using SimRegisPortal.Core.AppSettings.Components;
using SimRegisPortal.Domain.Entities;

namespace SimRegisPortal.Application.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly AccessTokenSettings _settings;

        public AccessTokenService(IOptions<AppSettings> options)
        {
            _settings = options.Value.AuthSettings.AccessToken;
        }

        public string GenerateToken(UserSession userSession)
        {
            var claims = new List<Claim>
            {
                new (CustomClaimTypes.IsAdmin, userSession.User.IsAdmin.ToString()),
                new (CustomClaimTypes.UserId, userSession.User.Id.ToString()),
                new (CustomClaimTypes.SessionId, userSession.Id.ToString()),
                new (CustomClaimTypes.Permissions, ""), // string.Join(Separators.UserPermissions, user.Permissions)
                new (CustomClaimTypes.ProjectPermissions, "") // string.Join(Separators.UserProjectPermissions, user.ProjectPermissions.Select(p => string.Concat(p.ProjectId, Separators.UserProjectPermission, p.PermissionId))) // p.ToString()?
            };

            return GenerateToken(claims);
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
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
