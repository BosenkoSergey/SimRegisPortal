using SimRegisPortal.Domain.Entities.Base;
using SimRegisPortal.Domain.Enums;
using SimRegisPortal.Domain.Helpers;

namespace SimRegisPortal.Domain.Entities
{
    public class UserAccount : BaseEntity<Guid>
    {
        public UserAccountStatus Status { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsAdmin { get; set; }

        public UserAccount()
        {
            Id = GuidHelper.Generate();
            Status = UserAccountStatus.Active;
        }
    }
}
