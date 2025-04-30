using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SimRegisPortal.Application.Constants;
using SimRegisPortal.Application.Models.Auth;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Context;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public bool IsAuthenticated { get; private set; }
    public bool IsAdmin { get; private set; }
    public Guid UserId { get; private set; }
    public HashSet<UserPermissionType> Permissions { get; private set; } = [];

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        RefreshProperties();
    }

    public async Task SignInAsync(AuthResponse auth)
    {
        var claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, auth.UserId.ToString()),
            new(CustomClaimTypes.IsAdmin, auth.IsAdmin.ToString()),
            new(CustomClaimTypes.Permissions, string.Join(Separators.UserPermissions, auth.Permissions))
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };

        await _httpContextAccessor.HttpContext!
            .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

        RefreshProperties();
    }

    public async Task SignOutAsync()
    {
        await _httpContextAccessor.HttpContext!
            .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        RefreshProperties();
    }

    public bool HasPermission(UserPermissionType permission)
    {
        return IsAdmin || Permissions.Contains(permission);
    }

    public bool HasAnyPermission(params UserPermissionType[] requiredPermissions)
    {
        return IsAdmin || Permissions.Overlaps(requiredPermissions);
    }

    private void RefreshProperties()
    {
        IsAuthenticated = _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        if (IsAuthenticated)
        {
            IsAdmin = GetClaimValue<bool>(CustomClaimTypes.IsAdmin);
            UserId = GetClaimValue<Guid>(CustomClaimTypes.UserId);
            Permissions = GetClaimHashSet<UserPermissionType>(CustomClaimTypes.Permissions, Separators.UserPermissions);
        }
        else
        {
            IsAdmin = false;
            UserId = Guid.Empty;
            Permissions = [];
        }
    }

    private T GetClaimValue<T>(string claimType)
    {
        var claimValue = _httpContextAccessor.HttpContext?.User.FindFirstValue(claimType);
        return ParseClaimPart<T>(claimValue);
    }

    private HashSet<T> GetClaimHashSet<T>(string claimType, char separator)
    {
        var value = GetClaimValue<string>(claimType);

        if (string.IsNullOrWhiteSpace(value))
        {
            return [];
        }

        return value
            .Split(separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(ParseClaimPart<T>)
            .ToHashSet();
    }

    private static T ParseClaimPart<T>(string? claimPart)
    {
        if (typeof(T) == typeof(Guid))
        {
            if (Guid.TryParse(claimPart, out var guidValue))
            {
                return (T)(object)guidValue;
            }
            throw new InvalidCastException($"Invalid GUID value '{claimPart}' for claim '{claimPart}'.");
        }

        if (typeof(T).IsEnum)
        {
            if (Enum.TryParse(typeof(T), claimPart, true, out var enumValue))
            {
                return (T)enumValue;
            }
            throw new InvalidCastException($"Invalid enum value '{claimPart}' for claim '{claimPart}'.");
        }

        try
        {
            return (T)Convert.ChangeType(claimPart!, typeof(T), CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            throw new InvalidCastException($"Failed to convert claim '{claimPart}' value '{claimPart}' to type {typeof(T)}.", ex);
        }
    }
}
