using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using Server.Application.Services;
using Server.Infrastructure.Options.Email;

namespace Server.Infrastructure.Services;

public sealed class EmailService(
    IConfiguration configuration,
    IOptions<EmailOptions> options) : IEmailService
{
    public void SendEmail(string fullName, string toEmail, string subject, string message)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(options.Value.SenderName, options.Value.SenderEmail));
        email.To.Add(new MailboxAddress(fullName, toEmail));

        email.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = message;
        email.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            client.Connect(
                options.Value.SmtpServer,
                options.Value.SmtpPort,
                SecureSocketOptions.StartTls);

            client.Authenticate(
                options.Value.SenderEmail,
                configuration["EmailAppPassword"]);

            client.Send(email);
            client.Disconnect(true);
        }
    }
}