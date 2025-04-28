namespace SimRegisPortal.Core.Exceptions;

public class CommonException : TemplatedException
{
    public CommonException(string resourceKey, params object[] parameters)
        : base(resourceKey, parameters)
    {
    }
}
