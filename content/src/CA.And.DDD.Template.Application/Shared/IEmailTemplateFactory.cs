using CA.And.DDD.Template.Domain.Enums;

namespace CA.And.DDD.Template.Application.Shared
{
    public interface IEmailTemplateFactory
    {
        Task<string> GetTemplateAsync(EmailTemplateType templateType);
    }
}
