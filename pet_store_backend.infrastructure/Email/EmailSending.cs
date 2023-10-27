using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using pet_store_backend.application.Common.Interfaces.Email;
using pet_store_backend.domain.Entities;

namespace pet_store_backend.infrastructure.Email;

public class EmailSending : IEmailService
{
    private readonly EmailSetting _emailSettings;
    public EmailSending(IOptions<EmailSetting> emailOptions)
    {
        _emailSettings = emailOptions.Value;
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Pet Store", _emailSettings.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

        return emailMessage;
    }
    public void SendEmail(Message message)
    {
        using var client = new SmtpClient();
        try
        {
            var emailMessage = CreateEmailMessage(message);
            client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_emailSettings.Username, _emailSettings.Password);

            client.Send(emailMessage);
        }
        catch
        {
            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}