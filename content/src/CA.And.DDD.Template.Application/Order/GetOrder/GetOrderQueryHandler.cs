using CA.And.DDD.Template.Application.Order.Shared;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain.Orders;
using MassTransit;

namespace CA.And.DDD.Template.Application.Order.GetOrder
{
    public sealed class GetOrderQueryHandler : IConsumer<GetOrderQuery>
    {
        private readonly ICacheService _cacheService;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderDomainService _orderDomainService;

        public GetOrderQueryHandler(ICacheService cacheService, IOrderRepository orderRepository, OrderDomainService orderDomainService)
        {
            _cacheService = cacheService;
            _orderRepository = orderRepository;
            _orderDomainService = orderDomainService;
        }

        /// <summary>
        /// This handler demonstrates the usage of the Cache Aside Pattern.
        /// First, we check if the data is available in the cache (Redis). If not,
        /// we retrieve the data from the database and store it in the cache.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="OrderNotFoundApplicationException"></exception>
        public async Task Consume(ConsumeContext<GetOrderQuery> query)
        {
            var cachedOder = await _cacheService.GetAsync<OrderDto>(CacheKeyBuilder.GetOrderKey(query.Message.Id));
            if (cachedOder is { })
            {
                await query.RespondAsync(cachedOder);
                return;
            }

            var id = new OrderId(query.Message.Id);
            var order = await _orderRepository.GetOrderById(id);
            await _orderDomainService.CalculateDiscountBaseOnLast31DaysSpendingAsync(order);

            await _cacheService.SetAsync(CacheKeyBuilder.GetOrderKey(query.Message.Id), order.ToDto());
            await query.RespondAsync(order.ToDto());
        }
    }
}
