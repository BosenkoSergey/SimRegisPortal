using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Core.Helpers;

namespace SimRegisPortal.Core.Entities;

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
        Refresh(lifetimeInDays);
    }

    public void Refresh(int lifetimeInDays)
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
