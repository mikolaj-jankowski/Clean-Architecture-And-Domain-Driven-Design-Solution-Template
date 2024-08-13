using Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents;
using MassTransit;

namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer.DomainEventHandlers
{
    public class CustomerCreatedDomainEventHandler : IConsumer<CustomerCreatedDomainEvent>
    {
        public Task Consume(ConsumeContext<CustomerCreatedDomainEvent> context)
        {
            return Task.CompletedTask;
        }
    }
}
