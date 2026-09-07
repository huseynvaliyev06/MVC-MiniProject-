using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MVC_MiniProject.Services.Interfaces;

namespace MVC_MiniProject.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            var host      = _config["SmtpSettings:Host"]!;
            var port      = int.Parse(_config["SmtpSettings:Port"]!);
            var userName  = _config["SmtpSettings:UserName"]!;
            var password  = _config["SmtpSettings:Password"]!;
            var fromName  = _config["SmtpSettings:FromName"]!;
            var fromEmail = _config["SmtpSettings:FromEmail"]!;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body    = new TextPart("html") { Text = htmlBody };

            using var client = new SmtpClient();
            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(userName, password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
