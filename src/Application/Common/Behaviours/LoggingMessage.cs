using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public enum LoggingEvents
{
    ApiRequest,
    PerformanceWarning,
    NotificationInformation,
    Information,
}

public static class LoggingMessage
{
    private static readonly Action<ILogger, string, Exception> ApiRequest =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId((int)LoggingEvents.ApiRequest, nameof(LoggingEvents.ApiRequest)),
            "{Message}");

    private static readonly Action<ILogger, string, Exception> NotificationRequest =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId((int)LoggingEvents.NotificationInformation, nameof(LoggingEvents.NotificationInformation)),
            "{Message}");

    private static readonly Action<ILogger, string, Exception> Information =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId((int)LoggingEvents.Information, nameof(LoggingEvents.Information)),
            "{Message}");

    private static readonly Action<ILogger, string, Exception> PerformanceWarning =
        LoggerMessage.Define<string>(
            LogLevel.Warning,
            new EventId((int)LoggingEvents.PerformanceWarning, nameof(LoggingEvents.PerformanceWarning)),
            "{Message}");

    public static void LogApiRequest(this ILogger logger, string message)
    {
        ApiRequest(logger, message, default!);
    }

    public static void LogPerformanceWarning(this ILogger logger, string message)
    {
        PerformanceWarning(logger, message, default!);
    }

    public static void LogNotificationInformation(this ILogger logger, string message)
    {
        NotificationRequest(logger, message, default!);
    }

    public static void LogInformation(this ILogger logger, string message)
    {
        Information(logger, message, default!);
    }
}