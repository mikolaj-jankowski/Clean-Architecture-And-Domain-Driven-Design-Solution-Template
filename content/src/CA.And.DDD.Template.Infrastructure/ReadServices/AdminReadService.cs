using CA.And.DDD.Template.Application.Order.BrowseCustomers;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Microsoft.EntityFrameworkCore;

namespace CA.And.DDD.Template.Infrastructure
{

    public class AdminReadService : Application.Shared.IAdminReadService
    {
        private readonly AppDbContext _dbContext;

        public AdminReadService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BrowseCustomersDto> BrowseCustomers(PaginationParameters paginationParameters, CancellationToken cancellationToken)
        {

            var query = _dbContext.Customers.AsQueryable();

            var totalCount = await query.CountAsync();

            if (string.IsNullOrWhiteSpace(paginationParameters.OrderColumn))
                query.OrderBy(x => x.CustomerId);

            var customerDtos = await query
                .AsNoTracking()
                .TagWithCallSite()
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .Select(customer => new BrowseCustomerDto(
                         customer.CustomerId.Value,
                         customer.FullName)
                ).ToListAsync();



            return new BrowseCustomersDto(customerDtos, totalCount);
        }

    }
}
