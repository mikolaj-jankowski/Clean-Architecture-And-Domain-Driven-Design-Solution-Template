using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain.Enums;
using CA.And.DDD.Template.Infrastructure.Exceptions;

namespace CA.And.DDD.Template.Infrastructure.Shared
{
    public class EmailTemplateFactory : IEmailTemplateFactory
    {
        private readonly string _templateDirectory = "EmailTemplates";

        public async Task<string> GetTemplateAsync(EmailTemplateType templateType)
        {
            var fileName = $"{templateType}.html";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CA.And.DDD.Template.Infrastructure", "EmailTemplates", fileName);

            if (!File.Exists(filePath))
            {
                throw new InfrastructureException($"Template '{fileName}' not found in '{_templateDirectory}'.");
            }

            return await File.ReadAllTextAsync(filePath);
        }

    }

}
