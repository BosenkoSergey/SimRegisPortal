using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.User;

public abstract record UserBaseDto
{
    public UserStatus Status { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Login { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public UserPermissionType[] Permissions { get; set; } = [];
}
