using LibraryApp.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace LibraryApp.Infrastructure.Logging;

public class SerilogLogger : IAppLogger
{
    private readonly ILogger<SerilogLogger> _logger;

    public SerilogLogger(ILogger<SerilogLogger> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    public void LogError(Exception exception, string message, params object[] args)
    {
        _logger.LogError(exception, message, args);
    }
}
