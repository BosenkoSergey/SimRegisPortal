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

    public static string ToClaimValue(
        this IEnumerable<UserProjectPermission> source)
    {
        return string.Join(Separators.UserProjectPermissions, source.Select(
            p => string.Concat(p.ProjectId, Separators.UserProjectPermission, (int)p.PermissionType)));
    }
}
