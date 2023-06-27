namespace WebCatalog.Logic.Common.ExternalServices.Email;

/// <summary>
/// Конфигурация почтового сервиса.
/// </summary>
public record EmailConfiguration
{
    public const string SectionName = "EmailConfiguration";

    public int Port { get; init; }

    public string Host { get; init; } = null!;

    public string Login { get; init; } = null!;

    public string Password { get; init; } = null!;

    public bool EnableSsl { get; init; }

    public string From { get; init; } = null!;
}