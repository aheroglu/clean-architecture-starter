namespace Server.Infrastructure.Options.Email;

public sealed class EmailOptions
{
    public string SmtpServer { get; set; } = default!;
    public int SmtpPort { get; set; }
    public string SenderName { get; set; } = default!;
    public string SenderEmail { get; set; } = default!;
    public string Username { get; set; } = default!;
}