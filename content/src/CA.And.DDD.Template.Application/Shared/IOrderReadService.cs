using CA.And.DDD.Template.Application.Order.BrowseOrders;
using CA.And.DDD.Template.Application.Order.GetOrder;

namespace CA.And.DDD.Template.Application.Shared
{
    public interface IOrderReadService
    {
        IQueryable<T> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken) where T : class;

        Task<BrowseOrdersDto> BrowseOrders(Guid customerId, int page, int pageSize, CancellationToken cancellationToken);
        Task<OrderDto> GetOrderById(Guid orderId, CancellationToken cancellationToken);
    }
}
