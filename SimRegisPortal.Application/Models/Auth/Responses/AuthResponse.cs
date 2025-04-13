namespace SimRegisPortal.Application.Models.Auth.Responses;

public record AuthResponse
{
    public Guid UserId { get; set; }
    public Guid RefreshToken { get; set; }
    public string AccessToken { get; set; } = null!;
    // TODO: Permissions and ProjectPermissions
}
