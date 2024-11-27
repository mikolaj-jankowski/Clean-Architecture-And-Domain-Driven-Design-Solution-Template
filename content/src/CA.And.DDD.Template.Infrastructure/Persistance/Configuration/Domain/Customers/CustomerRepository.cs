using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Infrastructure.Exceptions;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Microsoft.EntityFrameworkCore;

namespace CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
            => await _appDbContext.AddAsync(customer, cancellationToken);

        public async Task<Customer?> GetAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            var customer = await _appDbContext
            .Customers
            .Where(x => ((Guid)x.CustomerId) == customerId)
            .SingleOrDefaultAsync(cancellationToken);

            if (customer is null)
            {
                throw new NotFoundException(customerId);
            }

            return customer;
        }
    }
}
