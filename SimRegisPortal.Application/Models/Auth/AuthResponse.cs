using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Auth;

public sealed class AuthResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public Guid? EmployeeId { get; set; }
    public bool IsAdmin { get; set; }
    public HashSet<UserPermissionType> Permissions { get; set; } = [];
}
