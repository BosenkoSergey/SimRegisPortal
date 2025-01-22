using CMS.Core.AppSettings.Interfaces;
using Microsoft.Extensions.Options;

namespace CMS.Core.AppSettings
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        private readonly IOptions<AppSettings> _options;

        public AppSettingsProvider(IOptions<AppSettings> options)
        {
            _options = options;
        }

        public TSettings GetAppSettings<TSettings>() where TSettings : class, IAppSettings
        {
            return _options.Value as TSettings ?? throw new NullReferenceException(nameof(_options));
        }
    }
}
