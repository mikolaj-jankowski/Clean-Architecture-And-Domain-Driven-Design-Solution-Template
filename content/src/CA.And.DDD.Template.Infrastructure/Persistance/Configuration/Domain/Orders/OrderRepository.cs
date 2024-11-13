using CA.And.DDD.Template.Domain.Orders;
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

        public async Task AddAsync(Order order)
        {
            await _appDbContext.AddAsync(order);
        }
        public async Task<decimal> GetTotalSpentInLast31DaysAsync(Guid customerId)
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            decimal totalAmount = await _appDbContext.Orders
                .Where(order => order.OrderDate >= oneMonthAgo && (Guid)order.CustomerId == customerId)
                .SelectMany(m => m.OrderItems)
                .SumAsync(x => x.Price.Amount * x.Quantity);

            return totalAmount;

        }
    }
}
