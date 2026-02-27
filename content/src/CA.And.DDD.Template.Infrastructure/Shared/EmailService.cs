using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CA.And.DDD.Template.Infrastructure.Shared
{
    public class EmailService(IOptions<AppSettings> appSettings, IEmailTemplateFactory emailTemplateFactory) : IEmailService
    {
        private readonly Smtp _smtp = appSettings.Value.Smtp;
        private readonly IEmailTemplateFactory _emailTemplateFactory = emailTemplateFactory;

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_smtp.Server, _smtp.Port);
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

        public async Task SendWelcomeEmail(string to, Dictionary<string, string> replacements)
        {
            var emailTitle = "Welcome to Our Service!";
            var htmlTemplate = await _emailTemplateFactory.GetTemplateAsync(Domain.Enums.EmailTemplateType.WelcomeEmail);

            foreach (var replacement in replacements)
            {
                htmlTemplate = htmlTemplate.Replace($"{{{{{replacement.Key}}}}}", replacement.Value);
            }

            await SendEmailAsync(
                to,
                emailTitle,
                htmlTemplate);
        }
    }
}
