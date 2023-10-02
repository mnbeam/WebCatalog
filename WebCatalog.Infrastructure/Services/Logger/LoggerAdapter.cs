using Microsoft.Extensions.Logging;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Infrastructure.Services.Logger;

/// <inheritdoc />
public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly FileLogger _fileLogger;
    private readonly ILogger<T> _logger;

    /// <summary>
    /// Аддаптер для логгера.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="fileLogger"></param>
    public LoggerAdapter(
        ILogger<T> logger,
        FileLogger fileLogger)
    {
        _logger = logger;
        _fileLogger = fileLogger;
    }

    /// <summary>
    /// Залогировать ошибк на консоль и файл.
    /// </summary>
    /// <param name="messageError">Сообщение ошибки.</param>
    public void LogError(string messageError)
    {
        _logger.LogError(messageError);
        _fileLogger.LogError(messageError);
    }
}