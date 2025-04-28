namespace SimRegisPortal.Core.Exceptions;

public abstract class TemplatedException : Exception
{
    public string ResourceKey { get; }
    public object[] Parameters { get; }

    protected TemplatedException(string resourceKey, params object[] parameters)
    {
        ResourceKey = resourceKey;
        Parameters = parameters;
    }
}
