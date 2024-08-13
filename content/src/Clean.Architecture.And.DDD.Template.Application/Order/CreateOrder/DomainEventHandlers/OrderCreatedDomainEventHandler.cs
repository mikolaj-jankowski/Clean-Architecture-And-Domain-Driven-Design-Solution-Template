using Clean.Architecture.And.DDD.Template.Domian.Customers;
using Clean.Architecture.And.DDD.Template.Domian.Orders;
using Clean.Architecture.And.DDD.Template.Domian.Orders.DomainEvents;
using MassTransit;

namespace Clean.Architecture.And.DDD.Template.Application.Order.CreateOrder.DomainEventHandlers
{
    public sealed class OrderCreatedDomainEventHandler : IConsumer<OrderCreatedDomainEvent>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderCreatedDomainEventHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }
        public async Task Consume(ConsumeContext<OrderCreatedDomainEvent> context)
        {
            
        }
    }
}
