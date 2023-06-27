namespace WebCatalog.Logic.Common.ExternalServices;

/// <summary>
/// Логгер.
/// </summary>
public interface IAppLogger<T>
{
    void LogError(string message);
}