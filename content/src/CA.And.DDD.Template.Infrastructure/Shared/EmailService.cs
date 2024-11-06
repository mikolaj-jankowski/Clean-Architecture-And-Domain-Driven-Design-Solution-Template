using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CA.And.DDD.Template.Infrastructure.Shared
{
    public class EmailService : IEmailService
    {
        private readonly Smtp _smtp;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _smtp = appSettings.Value.Smtp;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var client = new SmtpClient(_smtp.Server, _smtp.Port))
            {
                client.Credentials = new NetworkCredential(_smtp.User, _smtp.Password);
                client.EnableSsl = _smtp.EnableSsl;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtp.EmailFrom),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
