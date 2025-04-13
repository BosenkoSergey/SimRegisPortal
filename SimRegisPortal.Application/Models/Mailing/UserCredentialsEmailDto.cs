using SimRegisPortal.Application.Models.Mailing.Common;

namespace SimRegisPortal.Application.Models.Mailing
{
    public record UserCredentialsEmailDto
    {
        public RecipientDto Recipient { get; init; } = null!;
        public string Login { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
