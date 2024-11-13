using CA.And.DDD.Template.Application.Order.Shared;
using CA.And.DDD.Template.Domain;
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
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            ILogger<CreateOrderCommandHandler> logger,
            IDateTimeProvider dateTimeProvider)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
        }


        public async Task Consume(ConsumeContext<CreateOrderCommand> command)
        {
            var orderTotalLast31Days = await _orderRepository.GetTotalSpentInLast31DaysAsync(command.Message.CustomerId);

            var order = Domain.Orders.Order.Create(
                new CustomerId(command.Message.CustomerId),
                new ShippingAddress(command.Message.Street, command.Message.PostalCode),
                new Money(orderTotalLast31Days),
                _dateTimeProvider.UtcNow);

            foreach(var product in command.Message.Products) 
            {
                order.AddOrderItem(product.ProductId, product.ProductName, product.Price, product.Currency, product.Quantity);
            }


            await _orderRepository.AddAsync(order);
            await command.RespondAsync<OrderDto>(new OrderDto(order.OrderId.Value, order.OrderItems.ToDto(), order.TotalAmount().Amount, order.TotalAmount().Currency));

            _logger.LogInformation("Created an order: {OrderId} ", order.OrderId);
        }


    }
}
