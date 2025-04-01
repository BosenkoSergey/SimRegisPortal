using SimRegisPortal.Domain.Entities.Base;
using SimRegisPortal.Domain.Enums;

namespace SimRegisPortal.Domain.Entities
{
    public class UserPermission : BaseEntity
    {
        public Guid UserId { get; set; }
        public UserPermissionType PermissionType { get; set; }

        public User User { get; set; } = default!;
    }
}
