namespace SimRegisPortal.Application.Context;

public interface IUserContext
{
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
    Guid UserId { get; }
    HashSet<int> Permissions { get; }
}
