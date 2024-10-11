//using CA.And.DDD.Template.Application.Customer.GetCustomer;
//using CA.And.DDD.Template.Application.Customer.Shared;
//using CA.And.DDD.Template.Application.Shared;
//using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
//using MassTransit;
//using Newtonsoft.Json;

//namespace CA.And.DDD.Template.Infrastructure.Queries.GetCustomer
//{
//    public sealed class GetCustomerQueryCacheFilter : IFilter<ConsumeContext<GetCustomerQuery>>
//    {
//        private readonly AppDbContext _appDbContext;
//        private readonly ICacheService _cacheService;

//        public GetCustomerQueryCacheFilter(AppDbContext appDbContext, ICacheService cacheService)
//        {
//            _appDbContext = appDbContext;
//            _cacheService = cacheService;
//        }

//        public void Probe(ProbeContext context)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task Send(ConsumeContext<GetCustomerQuery> context, IPipe<ConsumeContext<GetCustomerQuery>> next)
//        {
//            var cachedValue = await _cacheService.GetAsync(CustomerDto.CacheKey);
//            if (cachedValue != null)
//            {
//                await context.NotifyConsumed(context.ReceiveContext.ElapsedTime, TypeCache<GetCustomerQueryCacheFilter>.ShortName);
//                var customerDto = JsonConvert.DeserializeObject<CustomerDto>(cachedValue);
//                if(customerDto is { })
//                {
//                    await context.RespondAsync(customerDto);
//                }

//            }

//            await next.Send(context);

//        }
//    }
//}
