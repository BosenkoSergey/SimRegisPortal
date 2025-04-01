using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SimRegisPortal.Application.Constants;

namespace SimRegisPortal.Application.Context
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public bool IsAuthenticated { get; }
        public bool IsAdmin { get; }
        public Guid UserId { get; }
        public Guid UserSessionId { get; }
        public HashSet<int> Permissions { get; } = [];

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            IsAuthenticated = _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

            if (IsAuthenticated)
            {
                IsAdmin = GetClaimValue<bool>(CustomClaimTypes.IsAdmin);
                UserId = GetClaimValue<Guid>(CustomClaimTypes.UserId);
                UserSessionId = GetClaimValue<Guid>(CustomClaimTypes.SessionId);
                Permissions = GetClaimHashSet<int>(CustomClaimTypes.Permissions, Separators.UserPermissions);
            }
        }

        private T GetClaimValue<T>(string claimType)
        {
            var claimValue = _httpContextAccessor.HttpContext?.User.FindFirstValue(claimType);

            if (string.IsNullOrWhiteSpace(claimValue))
            {
                throw new UnauthorizedAccessException($"Missing claim: {claimType}");
            }

            if (typeof(T) == typeof(Guid))
            {
                if (Guid.TryParse(claimValue, out var guidValue))
                {
                    return (T)(object)guidValue;
                }
                throw new InvalidCastException($"Invalid GUID value '{claimValue}' for claim '{claimType}'.");
            }

            if (typeof(T).IsEnum)
            {
                if (Enum.TryParse(typeof(T), claimValue, true, out var enumValue))
                {
                    return (T)enumValue;
                }
                throw new InvalidCastException($"Invalid enum value '{claimValue}' for claim '{claimType}'.");
            }

            try
            {
                return (T)Convert.ChangeType(claimValue, typeof(T), CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Failed to convert claim '{claimType}' value '{claimValue}' to type {typeof(T)}.", ex);
            }
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
                .Select(v => (T)Convert.ChangeType(v, typeof(T), CultureInfo.InvariantCulture))
                .ToHashSet();
        }
    }
}
