using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace WeEatKholodets.Services;

public class EmailService : IEmailSender
{
    public EmailService(IConfiguration configuration)
    {
        _config= configuration;
    }

    private readonly IConfiguration _config;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("WeEatKholodets", "arvilla.keebler@ethereal.email"));
        emailMessage.To.Add(MailboxAddress.Parse("arvilla.keebler@ethereal.email"));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = htmlMessage
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}