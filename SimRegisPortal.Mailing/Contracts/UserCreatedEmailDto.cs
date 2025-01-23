using SimRegisPortal.Mailing.Contracts.Common;

namespace SimRegisPortal.Mailing.Contracts
{
    public record UserCreatedEmailDto(
        string RecipientEmail,
        string RecipientName,
        string Login,
        string Password
    ) : EmailRecipientDto(RecipientEmail, RecipientName);
}
