using CMS.Mailing.Contracts.Common;

namespace CMS.Mailing.Contracts
{
    public record UserCreatedEmailDto(
        string RecipientEmail,
        string RecipientName,
        string Login,
        string Password
    ) : EmailRecipientDto(RecipientEmail, RecipientName);
}
