using System.Security.Claims;

namespace SimRegisPortal.Application.Constants;

public static class CustomClaimTypes
{
    public const string IsAdmin = "is_admin";
    public const string UserId = ClaimTypes.NameIdentifier;
    public const string UserName = "user-name";
    public const string EmployeeId = "employee-id";
    public const string Permissions = "permissions";
}
