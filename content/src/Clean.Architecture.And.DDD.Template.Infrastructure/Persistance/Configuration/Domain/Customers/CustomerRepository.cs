using Clean.Architecture.And.DDD.Template.Domian.Customers;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.MsSql;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Customers
{
    public class CustomerRepository : ICustomerRespository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Customer customer)
        {
            await _appDbContext.AddAsync(customer);
        }
    }
}
