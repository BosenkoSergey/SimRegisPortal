using SimRegisPortal.Core.Enums;
using SimRegisPortal.Core.Extensions;
using SimRegisPortal.Core.Helpers;

namespace SimRegisPortal.Core.Entities;

public sealed class User : BaseEntity<Guid>
{
    public UserStatus Status { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public bool IsAdmin { get; set; }

    public ICollection<UserSession> Sessions { get; set; } = [];
    public ICollection<UserPermission> Permissions { get; set; } = [];
    public Employee? Employee { get; set; }

    public User()
    {
        Id = GuidHelper.Generate();
        Status = UserStatus.Active;
    }

    public void UpdatePermissions(IEnumerable<UserPermissionType> permissions)
    {
        Permissions.UpdateManyToMany(
            newKeys: permissions,
            getKey: link => link.PermissionType,
            createNewLink: type => new UserPermission { UserId = Id, PermissionType = type });
    }
}
