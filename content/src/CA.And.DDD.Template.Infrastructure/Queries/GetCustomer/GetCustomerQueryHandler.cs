using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Application.Customer.Shared;
using CA.And.DDD.Template.Application.Exceptions;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CA.And.DDD.Template.Infrastructure.Queries.GetCustomer
{
    public sealed class GetCustomerQueryHandler : IConsumer<GetCustomerQuery>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICacheService _cacheService;

        public GetCustomerQueryHandler(AppDbContext appDbContext, ICacheService cacheService)
        {
            _appDbContext = appDbContext;
            _cacheService = cacheService;
        }
        /// <summary>
        /// This handler demonstrates the usage of the Cache Aside Pattern.
        /// First, we check if the data is available in the cache (Redis). If not,
        /// we retrieve the data from the database and store it in the cache.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="CustomerNotFoundApplicationException"></exception>
        public async Task Consume(ConsumeContext<GetCustomerQuery> query)
        {
            var cachedCustomerDto = await _cacheService.GetAsync<CustomerDto>(CacheKeyBuilder.GetCustomerKey(query.Message.Email));
            if (cachedCustomerDto is { })
            {
                await query.RespondAsync(cachedCustomerDto);
                return;
            }

            var email = query.Message.Email;
            var customer = await _appDbContext
                .Set<Customer>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> ((string)x.Email) == email);

            if (customer is null)
            {
                throw new CustomerNotFoundApplicationException(email);
            }

            await _cacheService.SetAsync(CacheKeyBuilder.GetCustomerKey(query.Message.Email), customer.ToDto());
            await query.RespondAsync(customer.ToDto());
        }
    }
}
