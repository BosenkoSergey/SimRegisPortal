namespace SimRegisPortal.Application.Models.Auth;

public sealed class LoginRequest
{
    public string Login {  get; set; } = null!;
    public string Password { get; set; } = null!;
}
