using CA.And.DDD.Template.Application.Exceptions;
using CA.And.DDD.Template.Application.Shared;
using MassTransit;

namespace CA.And.DDD.Template.Application.Customer.GetCustomer
{
    public sealed class GetCustomerQueryHandler : IConsumer<GetCustomerQuery>
    {
        private readonly ICacheService _cacheService;
        private readonly ICustomerReadService _customerReadService;

        public GetCustomerQueryHandler(ICacheService cacheService, ICustomerReadService customerReadService)
        {
            _cacheService = cacheService;
            _customerReadService = customerReadService;
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
            var cachedCustomerDto = await _cacheService.GetAsync<CustomerDto>(CacheKeyBuilder.GetCustomerKey(query.Message.CustomerId), query.CancellationToken);
            if (cachedCustomerDto is { })
            {
                await query.RespondAsync(cachedCustomerDto);
                return;
            }

            var customerId = query.Message.CustomerId;
            var customerDto = await _customerReadService.GetCustomerById(customerId, query.CancellationToken);

            await _cacheService.SetAsync(CacheKeyBuilder.GetCustomerKey(customerId), customerDto);
            await query.RespondAsync(customerDto);
        }
    }
}
