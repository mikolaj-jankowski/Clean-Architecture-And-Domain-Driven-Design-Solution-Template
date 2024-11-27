using CA.And.DDD.Template.Application.Shared;
using MassTransit;

namespace CA.And.DDD.Template.Application.Order.GetOrder
{
    public sealed class GetOrderQueryHandler : IConsumer<GetOrderQuery>
    {
        private readonly ICacheService _cacheService;
        private readonly IOrderReadService _ordersReadService;

        public GetOrderQueryHandler(ICacheService cacheService, IOrderReadService ordersReadService)
        {
            _cacheService = cacheService;
            _ordersReadService = ordersReadService;
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
            var cachedOder = await _cacheService.GetAsync<OrderDto>(CacheKeyBuilder.GetOrderKey(query.Message.OrderId), query.CancellationToken);
            if (cachedOder is { })
            {
                await query.RespondAsync(cachedOder);
                return;
            }

            var orderDto = await _ordersReadService.GetOrderById(query.Message.OrderId, query.CancellationToken);

            await _cacheService.SetAsync(CacheKeyBuilder.GetOrderKey(query.Message.OrderId), orderDto);
            await query.RespondAsync(orderDto);
        }
    }
}
