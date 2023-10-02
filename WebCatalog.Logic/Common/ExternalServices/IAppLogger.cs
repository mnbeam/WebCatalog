namespace WebCatalog.Logic.Common.ExternalServices;

/// <summary>
/// Логгер.
/// </summary>
public interface IAppLogger<T>
{
    /// <summary>
    /// Залогировать ошибку.
    /// </summary>
    /// <param name="message">Сообщение ошибки.</param>
    void LogError(string message);
}