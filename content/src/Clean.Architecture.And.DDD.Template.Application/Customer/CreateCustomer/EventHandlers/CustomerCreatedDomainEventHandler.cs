using Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents;
using MassTransit;

namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer.EventHandlers
{
    public class CustomerCreatedDomainEventHandler : IConsumer<CustomerCreatedDomainEvent>
    {
        public Task Consume(ConsumeContext<CustomerCreatedDomainEvent> context)
        {
            return Task.CompletedTask;
        }
    }
}
