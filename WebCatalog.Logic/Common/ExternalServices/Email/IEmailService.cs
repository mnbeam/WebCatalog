namespace WebCatalog.Logic.Common.ExternalServices.Email;

/// <summary>
/// Почтовый сервис.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Отправить письмо.
    /// </summary>
    /// <param name="messageDto">Письмо.</param>
    Task SendAsync(EmailMessageDto messageDto);
}