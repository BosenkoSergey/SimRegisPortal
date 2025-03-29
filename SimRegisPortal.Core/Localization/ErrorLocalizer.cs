using Microsoft.Extensions.Localization;
using SimRegisPortal.Core.Resources;

namespace SimRegisPortal.Core.Localization
{
    public class ErrorLocalizer : IErrorLocalizer
    {
        private readonly IStringLocalizer<ErrorMessages> _messageLocalizer;

        public ErrorLocalizer(
            IStringLocalizer<ErrorMessages> messageLocalizer)
        {
            _messageLocalizer = messageLocalizer;
        }

        public string Localize(string resourceKey, params object[] parameters)
        {
            var localizedMessage = _messageLocalizer[resourceKey];

            return parameters.Length > 0
                ? string.Format(localizedMessage, parameters)
                : localizedMessage;
        }
    }
}
