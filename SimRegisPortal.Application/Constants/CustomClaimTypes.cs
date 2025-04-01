using System.Security.Claims;

namespace SimRegisPortal.Application.Constants
{
    public static class CustomClaimTypes
    {
        public const string IsAdmin = "is_admin";
        public const string UserId = ClaimTypes.NameIdentifier;
        public const string SessionId = "session_id";
        public const string Permissions = "permissions";
        public const string ProjectPermissions = "project_permissions";
    }
}
