namespace SimRegisPortal.Application.Models.Users;

public sealed record UserResponse : UserBaseDto
{
    public Guid Id { get; set; }
}
