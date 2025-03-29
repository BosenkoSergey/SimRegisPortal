using SimRegisPortal.Mailing.Contracts;

namespace SimRegisPortal.Mailing.Provider
{
    public interface IEmailProvider
    {
        Task SendUserCreatedEmailAsync(UserCredentialsEmailDto message);
        Task SendPasswordResetEmailAsync(UserCredentialsEmailDto message);
    }
}
