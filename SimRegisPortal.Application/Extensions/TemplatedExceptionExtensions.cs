using SimRegisPortal.Core.Exceptions.Base;
using SimRegisPortal.Core.Localization;

namespace SimRegisPortal.Application.Extensions
{
    public static class TemplatedExceptionExtensions
    {
        public static string GetLocalizedMessage(this TemplatedException exception, IErrorLocalizer errorLocalizer)
        {
            return errorLocalizer.Localize(exception.ResourceKey, exception.Parameters);
        }
    }
}
