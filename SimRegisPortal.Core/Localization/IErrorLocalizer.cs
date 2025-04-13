namespace SimRegisPortal.Core.Localization;

public interface IErrorLocalizer
{
    string Localize(string resourceKey, params object[] parameters);
}
