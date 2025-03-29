using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SimRegisPortal.Application.Constants;

namespace SimRegisPortal.Application.Context
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Lazy<Guid> _userAccountId;
        private readonly Lazy<Guid> _userSessionId;

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        public Guid UserAccountId => _userAccountId.Value;
        public Guid UserSessionId => _userSessionId.Value;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _userAccountId = new Lazy<Guid>(() => GetClaimValue<Guid>(CustomClaimTypes.UserAccountId));
            _userSessionId = new Lazy<Guid>(() => GetClaimValue<Guid>(CustomClaimTypes.UserSessionId));
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
    }
}
