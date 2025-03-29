using SimRegisPortal.Core.AppSettings.Components;
using SimRegisPortal.Core.AppSettings.Interfaces;

namespace SimRegisPortal.Core.AppSettings
{
    public record AppSettings : IAppSettings
    {
        public CompanyInfo CompanyInfo { get; init; }
        public ConnectionStrings ConnectionStrings { get; init; }
        public AuthSettings AuthSettings { get; init; }
        public SwaggerConfig SwaggerConfig { get; init; }
        public ExternalServices ExternalServices { get; init; }
    }
}
