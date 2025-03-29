using SimRegisPortal.Mailing.Models.Common;

namespace SimRegisPortal.Mailing.Contracts
{
    public record UserCredentialsEmailDto
    {
        public RecipientDto Recipient { get; init; } = null!;
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
