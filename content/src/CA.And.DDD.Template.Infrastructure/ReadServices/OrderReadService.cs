using CA.And.DDD.Template.Application.Order.BrowseOrders;
using CA.And.DDD.Template.Application.Order.GetOrder;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Infrastructure.Exceptions;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;

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
                .TagWithCallSite()
                .AsSplitQuery()
                .Include(x => x.OrderItems)
                .Where(x => (Guid)x.OrderId == orderId)
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
            {
                throw new NotFoundException(orderId);
            }

            return order.ToDto();
        }

        public async Task<BrowseOrdersDto> BrowseOrders(Guid customerId, PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {

            var query = _dbContext.Orders.AsQueryable();

            if (string.IsNullOrWhiteSpace(paginationParameters.OrderColumn))
                query.OrderBy(x => x.OrderId);
            else if (paginationParameters.GetOrdering.Contains("totalAmount"))
                query = query.OrderBy(x => x.TotalAmount.Amount);


            //TODO: Rewrite this query
            var orderDtos = await query
                .AsNoTracking()
                .TagWithCallSite()
                .Where(o => (Guid)o.CustomerId == customerId)
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .GroupBy(_ => 1)
                .Select(g => new BrowseOrdersDto(
                     g.OrderBy(x=> x.OrderId) 
                     .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                     .Take(paginationParameters.PageSize)
                     .Select(order => new BrowseOrderDto(
                         order.OrderId.Value,
                         order.OrderItems.Select(oi => new BrowseProductDto(oi.ProductName, oi.Quantity)).ToList(),
                         order.TotalAmount.Amount
                     ))
                     .ToList(),
                    g.Count()
                ))
                .FirstOrDefaultAsync(cancellationToken);


            return orderDtos;
        }
    }
}
