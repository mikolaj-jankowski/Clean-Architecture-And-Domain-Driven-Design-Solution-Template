using Clean.Architecture.And.DDD.Template.Domian.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Clean.Architecture.And.DDD.Template.Application.Order.CreateOrder
{

    public class CreateOrderCommandHandler : IConsumer<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }


        public async Task Consume(ConsumeContext<CreateOrderCommand> command)
        {
            var order = Domian.Orders.Order.Create(command.Message.CustomerId, command.Message.Street, command.Message.PostalCode);
            await _orderRepository.AddAsync(order);
            _logger.LogInformation($"Created an order: {order.OrderId}");
        }


    }
}
