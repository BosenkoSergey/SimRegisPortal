namespace SimRegisPortal.Application.Context;

public interface IUserContext
{
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
    Guid UserId { get; }
    Guid UserSessionId { get; }
    HashSet<int> Permissions { get; }
}
