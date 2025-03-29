namespace SimRegisPortal.Mailing.Models.Common
{
    public record RecipientDto
    {
        public string Email { get; init; } = null!;
        public string Name { get; init; } = null!;
    }
}
