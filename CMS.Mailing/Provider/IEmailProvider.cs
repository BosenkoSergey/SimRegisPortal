using CMS.Mailing.Contracts;

namespace CMS.Mailing.Provider
{
    public interface IEmailProvider
    {
        Task SendUserCreatedEmailAsync(UserCreatedEmailDto message);
        Task SendPasswordResetEmailAsync(PasswordResetEmailDto message);
    }
}
