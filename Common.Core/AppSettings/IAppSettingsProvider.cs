namespace Common.Core.AppSettings
{
    public interface IAppSettingsProvider
    {
        public TSettings GetAppSettings<TSettings>() where TSettings : class, IAppSettings;
    }
}
