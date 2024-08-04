using Clean.Architecture.And.DDD.Template.Domian.Orders.DomainEvents;
using MassTransit;

namespace Clean.Architecture.And.DDD.Template.Application.Order.CreateOrder.EventHandlers
{
    public sealed class OrderCreatedDomainEventHandler : IConsumer<OrderCreatedDomainEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedDomainEvent> context)
        {
            //TODO: Save user orders as Read Model
            throw new NotImplementedException();
        }
    }
}
