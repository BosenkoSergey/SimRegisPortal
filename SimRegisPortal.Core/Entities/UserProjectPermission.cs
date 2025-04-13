using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Core.Entities;

public sealed class UserProjectPermission : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public UserProjectPermissionType PermissionType { get; set; }

    public User User { get; set; } = default!;
    public Project Project { get; set; } = default!;
}
