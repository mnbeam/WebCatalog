namespace WebCatalog.Logic.Common.ExternalServices.Email;

/// <summary>
/// ДТО вложения.
/// </summary>
/// <param name="ContentStream">Данные.</param>
/// <param name="Name">Название вложения.</param>
/// <param name="MediaType">Формат.</param>
public record AttachmentDto(
    Stream ContentStream,
    string? Name,
    string? MediaType);