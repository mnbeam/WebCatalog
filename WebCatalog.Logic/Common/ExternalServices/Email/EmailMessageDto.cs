namespace WebCatalog.Logic.Common.ExternalServices.Email;

/// <summary>
/// ДТО письма.
/// </summary>
/// <param name="Subject">Тема.</param>
/// <param name="Recipient">Получатель.</param>
/// <param name="Body">Тело.</param>
/// <param name="Attachments">Вложения.</param>
public record EmailMessageDto(
    string Subject,
    string Recipient,
    string Body,
    List<AttachmentDto>? Attachments = null);