using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimRegisPortal.Application.Context;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Web.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class AuthorizeByPermissionsAttribute : Attribute, IAuthorizationFilter
{
    private readonly HashSet<UserPermissionType> _requiredPermissions;

    public AuthorizeByPermissionsAttribute(params UserPermissionType[] permissions)
    {
        _requiredPermissions = permissions.ToHashSet();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userContext = context.HttpContext.RequestServices.GetService<IUserContext>();
        if (userContext == null || !userContext.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (userContext.IsAdmin)
        {
            return;
        }

        if (!userContext.Permissions.Overlaps(_requiredPermissions))
        {
            context.Result = new ForbidResult();
        }
    }
}