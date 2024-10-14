using CA.And.DDD.Template.Application.Exceptions;
using CA.And.DDD.Template.Application.Order;
using CA.And.DDD.Template.Application.Order.GetOrder;
using CA.And.DDD.Template.Application.Order.Shared;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain.Orders;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CA.And.DDD.Template.Infrastructure.Queries.GetCustomer
{
    public sealed class GetOrderQueryHandler : IConsumer<GetOrderQuery>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICacheService _cacheService;

        public GetOrderQueryHandler(AppDbContext appDbContext, ICacheService cacheService)
        {
            _appDbContext = appDbContext;
            _cacheService = cacheService;
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
            }

            var id = query.Message.Id;
            var order = await _appDbContext
                .Set<Order>()
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OrderItems)
                .Where(x => ((Guid)x.OrderId) == id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new OrderNotFoundApplicationException(id);
            }

            await _cacheService.SetAsync(CacheKeyBuilder.GetOrderKey(query.Message.Id), order.ToDto());
            await query.RespondAsync(order.ToDto());
        }
    }
}
