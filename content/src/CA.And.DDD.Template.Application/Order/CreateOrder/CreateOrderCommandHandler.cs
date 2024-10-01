using CA.And.DDD.Template.Application.Customer.CreateCustomer;
using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Domain.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CA.And.DDD.Template.Application.Order.CreateOrder
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
            var order = Domain.Orders.Order.Create(
                new CustomerId(command.Message.CustomerId),
                new ShippingAddress(command.Message.Street, command.Message.PostalCode));

            foreach(var product in command.Message.Products) 
            {
                order.AddOrderItem(product.ProductId, product.ProductName, product.Price, product.Currency, product.Quantity);
            }
            await _orderRepository.AddAsync(order);
            await command.RespondAsync<CreateOrderResponse>(new CreateOrderResponse(order.OrderId.Value, order.OrderItems.MapToOrderItemDto()));

            _logger.LogInformation("Created an order: {OrderId} ", order.OrderId);
        }


    }
}
