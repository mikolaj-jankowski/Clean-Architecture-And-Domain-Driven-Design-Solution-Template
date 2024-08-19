using Clean.Architecture.And.DDD.Template.Domian.Orders.DomainEvents;
using MassTransit;

namespace Clean.Architecture.And.DDD.Template.Application.Order.CreateOrder.DomainEventHandlers
{
    public sealed class OrderCreatedDomainEventHandler : IConsumer<OrderCreatedDomainEvent>
    {

        public OrderCreatedDomainEventHandler()
        {
        }
        public async Task Consume(ConsumeContext<OrderCreatedDomainEvent> context)
        {
            //Sending e-mail.
        }
    }
}
