using SimRegisPortal.Mailing.Contracts;

namespace SimRegisPortal.Mailing.Provider
{
    public interface IEmailProvider
    {
        Task SendUserCreatedEmailAsync(UserCreatedEmailDto message);
        Task SendPasswordResetEmailAsync(PasswordResetEmailDto message);
    }
}
