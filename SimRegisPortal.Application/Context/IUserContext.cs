using SimRegisPortal.Application.Models.Auth;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Context;

public interface IUserContext
{
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
    Guid UserId { get; }
    HashSet<UserPermissionType> Permissions { get; }

    Task SignInAsync(AuthResponse auth);
    Task SignOutAsync();
}
