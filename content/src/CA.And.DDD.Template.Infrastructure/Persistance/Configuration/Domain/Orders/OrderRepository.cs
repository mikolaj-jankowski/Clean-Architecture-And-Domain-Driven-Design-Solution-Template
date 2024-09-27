using CA.And.DDD.Template.Domian.Orders;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;

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
    }
}
