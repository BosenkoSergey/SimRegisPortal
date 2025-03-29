namespace SimRegisPortal.Application.Context
{
    public interface IUserContext
    {
        bool IsAuthenticated { get; }
        Guid UserAccountId { get; }
        Guid UserSessionId { get; }
    }
}
