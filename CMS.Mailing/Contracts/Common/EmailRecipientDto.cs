namespace CMS.Mailing.Contracts.Common
{
    public record EmailRecipientDto(
        string RecipientEmail,
        string RecipientName
        );
}
