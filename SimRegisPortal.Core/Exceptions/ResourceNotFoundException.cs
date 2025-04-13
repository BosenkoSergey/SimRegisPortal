using SimRegisPortal.Core.Exceptions.Base;

namespace SimRegisPortal.Core.Exceptions;

public class ResourceNotFoundException : TemplatedException
{
    public ResourceNotFoundException(string resourceName)
        : base("Exception.Resource.NotFound", resourceName)
    {
    }
}
