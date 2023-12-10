using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Vezeeta.Core.Domain.Users;
using Vezeeta.Core.Service;

namespace Vezeeta.Service.Mail
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendAsync(string subject, string message, User user)
        {
            var apiKey = _configuration["Mail:ApiKey"];

            var client = new SendGridClient(apiKey);
            var text = message;

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("---your send grid email here---", "Vezeeta"),
                Subject = subject,
                PlainTextContent = text,
                HtmlContent = text.Replace("\n", "<br>")
            };

            msg.AddTo(new EmailAddress(user.Email, user.FullName));
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }
    }
}
