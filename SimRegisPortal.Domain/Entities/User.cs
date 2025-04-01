using SimRegisPortal.Domain.Entities.Base;
using SimRegisPortal.Domain.Enums;
using SimRegisPortal.Domain.Helpers;

namespace SimRegisPortal.Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        public UserStatus Status { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsAdmin { get; set; }

        public ICollection<UserSession> Sessions { get; set; } = [];
        public ICollection<UserPermission> Permissions { get; set; } = [];
        public ICollection<UserProjectPermission> ProjectPermissions { get; set; } = [];

        public User()
        {
            Id = GuidHelper.Generate();
            Status = UserStatus.Active;
        }
    }
}
