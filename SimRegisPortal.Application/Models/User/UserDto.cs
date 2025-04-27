using SimRegisPortal.Application.Models.Base;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.User;

public sealed class UserDto : BaseEntityDto<Guid>
{
    public UserStatus Status { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Login { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public UserPermissionType[] Permissions { get; set; } = [];
}
