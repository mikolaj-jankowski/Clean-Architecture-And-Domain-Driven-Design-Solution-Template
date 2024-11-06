using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain.Customers.DomainEvents;
using MassTransit;

namespace CA.And.DDD.Template.Application.Customer.CreateCustomer.DomainEventHandlers
{
    public class CustomerCreatedDomainEventHandler : IConsumer<CustomerCreatedDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateFactory _emailTemplateFactory;

        public CustomerCreatedDomainEventHandler(IEmailService emailService, IEmailTemplateFactory emailTemplateFactory)
        {
            _emailService = emailService;
            _emailTemplateFactory = emailTemplateFactory;
        }
        public async Task Consume(ConsumeContext<CustomerCreatedDomainEvent> context)
        {
            //We use Mailhog to send email's, to see them please go to: http://localhost:8025

            await SendWelcomeEmail(context.Message); 

            // You could also include other logic here that should be part 
            // of the eventual consistency pattern.

        }

        private async Task SendWelcomeEmail(CustomerCreatedDomainEvent @event)
        {
            var htmlTemplate = await _emailTemplateFactory.GetTemplateAsync(Domain.Enums.EmailTemplateType.WelcomeEmail);

            var replacements = new Dictionary<string, string>
            {
                { "FullName", @event.FullName }
            };

            foreach (var replacement in replacements)
            {
                htmlTemplate = htmlTemplate.Replace($"{{{{{replacement.Key}}}}}", replacement.Value);
            }

            await _emailService.SendEmailAsync(
                @event.Email,
                "Welcome to Our Service!",
                htmlTemplate);
        }
    }
}
