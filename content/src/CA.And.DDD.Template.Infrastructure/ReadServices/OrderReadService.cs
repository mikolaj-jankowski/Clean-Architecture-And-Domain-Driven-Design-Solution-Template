using CA.And.DDD.Template.Application.Order.BrowseOrders;
using CA.And.DDD.Template.Application.Order.GetOrder;
using CA.And.DDD.Template.Infrastructure.Exceptions;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Microsoft.EntityFrameworkCore;

namespace CA.And.DDD.Template.Infrastructure.ReadServices
{
    public class OrderReadService : Application.Shared.IOrderReadService
    {
        private readonly AppDbContext _dbContext;

        public OrderReadService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken) where T : class
        {
            return _dbContext.Set<T>()
                .FromSqlRaw(sql, parameters)
                .AsNoTracking();
        }

        public async Task<OrderDto> GetOrderById(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .TagWith(nameof(GetOrderById))
                .AsSplitQuery()
                .Include(x => x.OrderItems)
                .Where(x => (Guid)x.OrderId == orderId)
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
            {
                throw new NotFoundException(orderId.ToString());
            }

            return order.ToDto();
        }

        public async Task<BrowseOrdersDto> BrowseOrders(Guid customerId, CancellationToken cancellationToken)
        {
            var orderDtos = await _dbContext.Orders
                .AsNoTracking()
                .Where(o => (Guid)o.CustomerId == customerId)
                .Select(o =>
                new BrowseOrderDto(o.OrderId.Value))
                .ToListAsync(cancellationToken);

            return new BrowseOrdersDto(orderDtos);
        }
    }
}
