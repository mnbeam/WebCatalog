using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.Common.ExternalServices.Email;

namespace WebCatalog.Infrastructure.Services;

/// <inheritdoc />
public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;

    private readonly IAppLogger<EmailService> _logger;

    public EmailService(
        IOptions<EmailConfiguration> emailConfiguration, 
        IAppLogger<EmailService> logger)
    {
        _logger = logger;
        _emailConfiguration = emailConfiguration.Value;
    }

    /// <inheritdoc />
    public async Task SendAsync(EmailMessageDto messageDto)
    {
        try
        {
            using var client = CreateSmtpClientAsync();
            var message = GetMessage(messageDto);

            try
            {
                throw new SmtpException();
                await client.SendMailAsync(message);
            }
            catch (SmtpException e)
            {
                _logger.LogError($"Error while sending email: {e.Message}");
                throw;
            }
        }
        catch (Exception e)
        {
            _logger.LogError($"Error smtp-client: {e.Message}");
            throw;
        }
    }

    private MailMessage GetMessage(EmailMessageDto messageDto)
    {
        var sender = new MailAddress(_emailConfiguration.From, _emailConfiguration.Login);
        var message = new MailMessage(sender, new MailAddress(messageDto.Recipient))
        {
            Subject = messageDto.Subject,
            BodyEncoding = Encoding.UTF8,
            Body = messageDto.Body,
            IsBodyHtml = true,
            SubjectEncoding = Encoding.UTF8,
        };

        if (messageDto.Attachments != null)
        {
            foreach (var attachment in messageDto.Attachments)
            {
                message.Attachments.Add(new Attachment(
                    attachment.ContentStream,
                    attachment.Name,
                    attachment.MediaType));
            }
        }
        
        return message;
    }

    private SmtpClient CreateSmtpClientAsync()
    {
        var credentials = new NetworkCredential(_emailConfiguration.Login,
            _emailConfiguration.Password);
        var cache = new CredentialCache
        {
            { _emailConfiguration.Host, _emailConfiguration.Port, "Login", credentials }
        };

        return new SmtpClient()
        {
            Host = _emailConfiguration.Host,
            Port = _emailConfiguration.Port,
            EnableSsl = _emailConfiguration.EnableSsl,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = cache,
        };
    }
}