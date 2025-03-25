namespace LibraryApp.Application.Common.Interfaces;

public interface IAppLogger
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(Exception exception, string message, params object[] args);
}
