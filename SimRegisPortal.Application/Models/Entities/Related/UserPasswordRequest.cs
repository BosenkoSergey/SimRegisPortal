namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed record UserPasswordRequest
{
    public string Password { get; set; } = null!;
}
