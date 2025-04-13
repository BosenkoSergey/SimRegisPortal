using SimRegisPortal.Application.Settings.Components;

namespace SimRegisPortal.Core.Settings;

public record AppSettings
{
    public CompanyInfo CompanyInfo { get; init; } = null!;
    public ConnectionStrings ConnectionStrings { get; init; } = null!;
    public AuthSettings AuthSettings { get; init; } = null!;
    public SwaggerConfig SwaggerConfig { get; init; } = null!;
    public ExternalServices ExternalServices { get; init; } = null!;
}
