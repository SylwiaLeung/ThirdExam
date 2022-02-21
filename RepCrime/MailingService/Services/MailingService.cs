using MailingService.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MailingService.Services
{
    public class MailingService : IMailingService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<MailingService> _logger;
        private readonly IConfiguration _configuration;
        public MailingService(IOptions<EmailSettings> emailSettings, ILogger<MailingService> logger, IConfiguration configuration)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var message = new MimeMessage();

            _logger.LogDebug($"FromName: {_emailSettings.FromName ?? "null"}, FromAddress{_emailSettings.FromAddress ?? "null"}");
            _logger.LogDebug($"ToName: {email.ToName ?? "null"}, ToAddress:{email.ToAddress ?? "null"}");

            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            message.To.Add(new MailboxAddress(email.ToName, email.ToAddress));
            message.Subject = email.Subject;
            message.Body = new TextPart("plain")
            {
                Text = email.Body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.AuthenticateAsync(_configuration["EmailRecipient:Username"], _configuration["EmailRecipient:Password"]);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            return true;
        }
    }
}
