namespace SimRegisPortal.Application.Context;

public interface IUserContext
{
    bool IsAuthenticated { get; }
    Guid UserId { get; }
    Guid UserSessionId { get; }
}
