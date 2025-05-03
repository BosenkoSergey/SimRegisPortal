using SimRegisPortal.Application.Models.Auth;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Context;

public interface IUserContext
{
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
    Guid UserId { get; }
    string UserName { get; }
    HashSet<UserPermissionType> Permissions { get; }

    bool HasPermission(UserPermissionType permission);
    bool HasAnyPermission(params UserPermissionType[] requiredPermissions);
}
