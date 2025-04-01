using SimRegisPortal.Domain.Entities;

namespace SimRegisPortal.Application.Services.Interfaces
{
    public interface IAccessTokenService
    {
        string GenerateToken(UserSession userSession);
    }
}
