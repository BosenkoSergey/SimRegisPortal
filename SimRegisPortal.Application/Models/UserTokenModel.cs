namespace SimRegisPortal.Application.Models
{
    public record UserTokenModel
    {
        public Guid UserId { get; init; }
        public Guid SessionId { get; init; }
    }
}
