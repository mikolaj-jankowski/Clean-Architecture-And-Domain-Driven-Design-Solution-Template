using CA.And.DDD.Template.Application.Customer.Shared;
using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Infrastructure.Exceptions;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Microsoft.EntityFrameworkCore;

namespace CA.And.DDD.Template.Infrastructure
{

    public class CustomerReadService : Application.Shared.ICustomerReadService
    {
        private readonly AppDbContext _dbContext;

        public CustomerReadService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken) where T : class
        {
            return _dbContext.Set<T>()
                .FromSqlRaw(sql, parameters)
                .AsNoTracking();
        }

        public async Task<CustomerDto> GetCustomerById(Guid customerId, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.Customers
                .TagWithCallSite()
                .AsSplitQuery()
                .Where(o => (Guid)o.CustomerId == customerId)
                .FirstOrDefaultAsync(cancellationToken);

            if (customer is null)
            {
                throw new NotFoundException(customerId);
            }

            return customer.ToDto();
        }

    }
}
