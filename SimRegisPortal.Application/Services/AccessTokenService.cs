using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimRegisPortal.Application.Constants;
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Application.Settings.Components;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Settings;

namespace SimRegisPortal.Application.Services;

public sealed class AccessTokenService : IAccessTokenService
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
            new (CustomClaimTypes.Permissions, userSession.User.Permissions.ToClaimValue())
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
