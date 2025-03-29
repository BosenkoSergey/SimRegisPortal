using SimRegisPortal.Application.Models;

namespace SimRegisPortal.Application.Services.Interfaces
{
    public interface IAccessTokenService
    {
        string GenerateToken(UserTokenModel user);
    }
}
