using SimRegisPortal.Application.Constants;
using SimRegisPortal.Core.Entities;

namespace SimRegisPortal.Application.Extensions;

public static class PermissionExtensions
{
    public static string ToClaimValue(
        this IEnumerable<UserPermission> source)
    {
        return string.Join(Separators.UserPermissions, source.Select(p => (int)p.PermissionType));
    }
}
