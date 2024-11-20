namespace Server.Application.Services;

public interface IEmailService
{
    void SendEmail(string fullName, string toEmail, string subject, string message);
}