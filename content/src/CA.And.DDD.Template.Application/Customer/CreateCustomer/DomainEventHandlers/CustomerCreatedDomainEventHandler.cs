using CA.And.DDD.Template.Domian.Customers.DomainEvents;
using MassTransit;

namespace CA.And.DDD.Template.Application.Customer.CreateCustomer.DomainEventHandlers
{
    public class CustomerCreatedDomainEventHandler : IConsumer<CustomerCreatedDomainEvent>
    {
        public Task Consume(ConsumeContext<CustomerCreatedDomainEvent> context)
        {
            return Task.CompletedTask;
        }
    }
}
