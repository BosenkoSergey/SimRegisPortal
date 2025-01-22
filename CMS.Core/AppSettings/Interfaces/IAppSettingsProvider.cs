namespace CMS.Core.AppSettings.Interfaces
{
    public interface IAppSettingsProvider
    {
        public TSettings GetAppSettings<TSettings>() where TSettings : class, IAppSettings;
    }
}
