using SimRegisPortal.Domain.Entities.Base;
using SimRegisPortal.Domain.Helpers;

namespace SimRegisPortal.Domain.Entities
{
    public class UserSession : BaseEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public Guid RefreshToken { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public User User { get; set; } = default!;

        public bool IsActive => ExpiresAt > DateTime.UtcNow;

        private UserSession() { }

        public UserSession(Guid userId, int lifetimeInDays)
        {
            Id = GuidHelper.Generate();
            UserId = userId;
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
