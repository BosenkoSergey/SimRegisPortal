using SimRegisPortal.Application.Models.Mailing;

namespace SimRegisPortal.Application.Services.Interfaces;

public interface IEmailService
{
    Task SendUserCreatedEmailAsync(UserCredentialsEmailDto message);
    Task SendPasswordResetEmailAsync(UserCredentialsEmailDto message);
}
