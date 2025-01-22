using CMS.Core.AppSettings.Components;
using CMS.Core.AppSettings.Interfaces;

namespace CMS.Core.AppSettings
{
    public record AppSettings(
        ConnectionStrings ConnectionStrings,
        SwaggerConfig SwaggerConfig
    ) : IAppSettings;
}
