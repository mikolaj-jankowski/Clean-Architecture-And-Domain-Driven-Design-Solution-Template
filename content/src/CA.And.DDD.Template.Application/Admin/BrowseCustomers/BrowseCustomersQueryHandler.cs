using CA.And.DDD.Template.Application.Shared;
using MassTransit;

namespace CA.And.DDD.Template.Application.Order.BrowseCustomers
{
    public class BrowseCustomersQueryHandler : IConsumer<BrowseCustomersQuery>
    {
        private readonly IAdminReadService _adminReadService;

        public BrowseCustomersQueryHandler(IAdminReadService adminReadService)
        {
            _adminReadService = adminReadService;
        }

        public async Task Consume(ConsumeContext<BrowseCustomersQuery> query)
        {
            var orders = await _adminReadService.BrowseCustomers(
                query.Message.PaginationParameters,
                query.CancellationToken);

            await query.RespondAsync(orders);
        }
    }
}
