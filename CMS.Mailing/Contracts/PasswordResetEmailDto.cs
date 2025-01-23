using CMS.Mailing.Contracts.Common;

namespace CMS.Mailing.Contracts
{
    public record PasswordResetEmailDto(
        string RecipientEmail,
        string RecipientName,
        string Login,
        string Password
    ) : EmailRecipientDto(RecipientEmail, RecipientName);
}
