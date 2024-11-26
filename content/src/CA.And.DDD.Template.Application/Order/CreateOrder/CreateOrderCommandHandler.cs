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
        private readonly OrderDomainService _orderDomainService;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository,
            ILogger<CreateOrderCommandHandler> logger,
            IDateTimeProvider dateTimeProvider, 
            OrderDomainService orderDomainService)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _dateTimeProvider = dateTimeProvider;
            _orderDomainService = orderDomainService;
        }


        public async Task Consume(ConsumeContext<CreateOrderCommand> command)
        {
            var orderTotalLast31Days = await _orderRepository.GetTotalSpentInLast31DaysAsync(command.Message.CustomerId, command.CancellationToken);

            var order = Domain.Orders.Order.Create(
                new CustomerId(command.Message.CustomerId),
                new ShippingAddress(command.Message.Street, command.Message.PostalCode),
                _dateTimeProvider.UtcNow);

            foreach(var product in command.Message.Products) 
            {
                order.AddOrderItem(product.ProductId, product.ProductName, product.Price, product.Currency, product.Quantity);
            }

            await _orderDomainService.CalculateDiscountBaseOnLast31DaysSpendingAsync(order, command.CancellationToken);
            await _orderRepository.AddAsync(order, command.CancellationToken);

            //await command.RespondAsync<OrderDto>(order.ToDto());

            _logger.LogInformation("Created an order: {OrderId} ", order.OrderId);
        }

    }
}
