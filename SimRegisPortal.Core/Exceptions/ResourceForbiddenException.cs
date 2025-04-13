using SimRegisPortal.Core.Exceptions.Base;

namespace SimRegisPortal.Core.Exceptions;

public class ResourceForbiddenException : TemplatedException
{
    public ResourceForbiddenException(string resourceName)
        : base("Exception.Resource.Forbidden", resourceName)
    {
    }
}
