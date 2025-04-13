using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Core.Entities;

public sealed class UserPermission : BaseEntity
{
    public Guid UserId { get; set; }
    public UserPermissionType PermissionType { get; set; }

    public User User { get; set; } = default!;
}
