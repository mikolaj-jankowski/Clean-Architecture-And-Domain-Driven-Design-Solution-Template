using CA.And.DDD.Template.Application.Shared;
using MassTransit;

namespace CA.And.DDD.Template.Application.Order.BrowseOrders
{
    public class BrowseOrdersQueryHandler : IConsumer<BrowseOrdersQuery>
    {
        private readonly IOrderReadService _ordersReadService;

        public BrowseOrdersQueryHandler(IOrderReadService ordersReadService)
        {
            _ordersReadService = ordersReadService;
        }
        public async Task Consume(ConsumeContext<BrowseOrdersQuery> query)
        {
            var orders = await _ordersReadService.BrowseOrders(query.Message.CustomerId, query.CancellationToken);

            await query.RespondAsync(orders);
        }
    }
}
