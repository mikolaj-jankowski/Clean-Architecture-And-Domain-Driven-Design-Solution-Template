using CA.And.DDD.Template.Application.Customer.Shared;
using CA.And.DDD.Template.Application.Exceptions;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain.Customers;
using MassTransit;

namespace CA.And.DDD.Template.Application.Customer.GetCustomer
{
    public sealed class GetCustomerQueryHandler : IConsumer<GetCustomerQuery>
    {
        private readonly ICacheService _cacheService;
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICacheService cacheService, ICustomerRepository customerRepository)
        {
            _cacheService = cacheService;
            _customerRepository = customerRepository;
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
            var customer = (await _customerRepository.GetAsync(email))!.ToDto();

            await _cacheService.SetAsync(CacheKeyBuilder.GetCustomerKey(query.Message.Email), customer);
            await query.RespondAsync(customer);
        }
    }
}
