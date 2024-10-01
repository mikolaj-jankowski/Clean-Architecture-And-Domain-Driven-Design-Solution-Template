using CA.And.DDD.Template.Domain.Customers.DomainEvents;
using MassTransit;

namespace CA.And.DDD.Template.Application.Customer.VerifyEmail.DomainEventHandlers
{
    public class CustomerEmailVerifiedChangedDomainEventHandler : IConsumer<CustomerEmailVerifiedDomainEvent>
    {
        public Task Consume(ConsumeContext<CustomerEmailVerifiedDomainEvent> context)
        {
            //Here, you could send an email to the customer informing them about the successful verification process.

            // You could also include other logic here that should be part 
            // of the eventual consistency pattern.

            return Task.CompletedTask;
        }
    }
}
