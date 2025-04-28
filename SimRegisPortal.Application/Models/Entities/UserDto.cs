using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Entities;

public sealed class UserDto : BaseEntityDto<Guid>
{
    public UserStatus Status { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Login { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public List<UserPermissionType> Permissions { get; set; } = [];
}
