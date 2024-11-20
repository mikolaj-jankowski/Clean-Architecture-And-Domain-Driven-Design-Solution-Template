using CA.And.DDD.Template.Domain.Orders;
using CA.And.DDD.Template.Infrastructure.Exceptions;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Microsoft.EntityFrameworkCore;

namespace CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _appDbContext.AddAsync(order, cancellationToken);
        }

        public async Task<Order> GetOrderById(OrderId orderId, CancellationToken cancellationToken = default)
        {
            var order = await _appDbContext
                .Orders
                .TagWith(nameof(GetOrderById))
                .AsSplitQuery()
                .Include(x => x.OrderItems)
                .Where(x => ((Guid)x.OrderId) == orderId.Value)
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
            {
                throw new NotFoundException(orderId.Value.ToString());
            }

            return order;
         }

        public async Task<decimal> GetTotalSpentInLast31DaysAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            decimal totalAmount = await _appDbContext.Orders
                .Where(order => order.OrderDate >= oneMonthAgo && (Guid)order.CustomerId == customerId)
                .SelectMany(m => m.OrderItems)
                .SumAsync(x => x.Price.Amount * x.Quantity, cancellationToken);

            return totalAmount;

        }
    }
}
