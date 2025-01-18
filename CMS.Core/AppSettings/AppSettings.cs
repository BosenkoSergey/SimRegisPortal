using CMS.Core.AppSettings.Components;
using Common.Core.AppSettings;

namespace CMS.Core.AppSettings
{
    public record AppSettings(
        ConnectionStrings ConnectionStrings,
        SwaggerConfig SwaggerConfig
    ) : IAppSettings;
}
