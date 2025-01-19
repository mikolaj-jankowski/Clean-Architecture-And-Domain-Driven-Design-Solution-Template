using CA.And.DDD.Template.Application.Shared;

namespace CA.And.DDD.Template.Application.Order.BrowseOrders
{
    public sealed record BrowseOrdersQuery(Guid CustomerId, PaginationParameters PaginationParameters);
}
