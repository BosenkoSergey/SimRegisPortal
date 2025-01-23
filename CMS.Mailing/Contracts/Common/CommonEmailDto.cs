namespace CMS.Mailing.Contracts.Common
{
    public record CommonEmailDto(
        string RecipientEmail,
        string RecipientName,
        string Subject,
        string PlainTextContent,
        string HtmlContent
    ) : EmailRecipientDto(RecipientEmail, RecipientName);
}
