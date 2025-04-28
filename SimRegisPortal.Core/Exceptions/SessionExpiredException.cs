namespace SimRegisPortal.Core.Exceptions;

public class SessionExpiredException : TemplatedException
{
    public SessionExpiredException()
        : base("Exception.UserSession.Expired")
    {
    }
}
