using SimRegisPortal.Domain.Entities.Base;
using SimRegisPortal.Domain.Helpers;

namespace SimRegisPortal.Domain.Entities
{
    public class UserSession : BaseEntity<Guid>
    {
        public Guid UserAccountId { get; private set; }
        public Guid RefreshToken { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public UserAccount UserAccount { get; set; } = default!;

        public bool IsActive => ExpiresAt > DateTime.UtcNow;

        private UserSession() { }

        public UserSession(Guid userAccountId, int lifetimeInDays)
        {
            Id = GuidHelper.Generate();
            UserAccountId = userAccountId;
            CreatedAt = DateTime.UtcNow;
            RotateRefreshToken(lifetimeInDays);
        }

        public void RotateRefreshToken(int lifetimeInDays)
        {
            RefreshToken = Guid.NewGuid();
            UpdatedAt = DateTime.UtcNow;
            ExpiresAt = UpdatedAt.AddDays(lifetimeInDays);
        }

        public void FinishSession()
        {
            UpdatedAt = ExpiresAt = DateTime.UtcNow;
        }
    }
}
