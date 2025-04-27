using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Auth;

public sealed class AuthResponse
{
    public Guid UserId { get; set; }
    public Guid RefreshToken { get; set; }
    public string AccessToken { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public UserPermissionType[] Permissions { get; set; } = [];
}
