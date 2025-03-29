using System.Security.Claims;

namespace SimRegisPortal.Application.Constants
{
    public static class CustomClaimTypes
    {
        public const string UserAccountId = ClaimTypes.NameIdentifier;
        public const string UserSessionId = "session_id";
    }
}
