namespace SimRegisPortal.Application.Models.User;

public sealed record UserResponse : UserBaseDto
{
    public Guid Id { get; set; }
}
