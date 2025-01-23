using CMS.Core.AppSettings.Components;
using CMS.Core.AppSettings.Interfaces;

namespace CMS.Core.AppSettings
{
    public record AppSettings : IAppSettings
    {
        public CompanyInfo CompanyInfo { get; init; }
        public ConnectionStrings ConnectionStrings { get; init; }
        public SwaggerConfig SwaggerConfig { get; init; }
        public ExternalServices ExternalServices { get; init; }
    }
}
