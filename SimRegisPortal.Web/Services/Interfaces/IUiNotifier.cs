namespace SimRegisPortal.Web.Services.Interfaces;

public interface IUiNotifier
{
    Task Success(string message);
    Task Info(string message);
    Task Warning(string message);
    Task Error(string message);
    Task Exception(Exception exception);
}