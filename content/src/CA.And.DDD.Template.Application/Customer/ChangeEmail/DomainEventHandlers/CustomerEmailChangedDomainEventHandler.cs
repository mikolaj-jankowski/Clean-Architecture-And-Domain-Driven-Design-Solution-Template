using CA.And.DDD.Template.Domain.Customers.DomainEvents;
using MassTransit;

namespace CA.And.DDD.Template.Application.Customer.ChangeEmail.DomainEventHandlers
{
    public class CustomerEmailChangedDomainEventHandler : IConsumer<CustomerEmailChangedDomainEvent>
    {
        public Task Consume(ConsumeContext<CustomerEmailChangedDomainEvent> context)
        {
            //Here, you could send an emails to old and new e-email addresses
            //informing about the correct change of the email address.

            // You could also include other logic here that should be part 
            // of the eventual consistency pattern.

            return Task.CompletedTask;
        }
    }
}
