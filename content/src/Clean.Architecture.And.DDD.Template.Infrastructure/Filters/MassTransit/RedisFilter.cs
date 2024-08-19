using Clean.Architecture.And.DDD.Template.Domian;
using MassTransit;
using StackExchange.Redis;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Filters.MassTransit
{
    public class RedisFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IConnectionMultiplexer _multiplexer;
        private readonly string _ordersCounterKey = String.Empty;

        public RedisFilter(IDateTimeProvider dateTimeProvider, IConnectionMultiplexer multiplexer)
        {
            _dateTimeProvider = dateTimeProvider;
            _multiplexer = multiplexer;
            _ordersCounterKey = $"total_requests_counter_{_dateTimeProvider.UtcNow.ToShortDateString()}";
        }
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            await next.Send(context); 

            var db = _multiplexer.GetDatabase();
            var ordersCounter = await db.StringGetAsync(_ordersCounterKey);
            if (ordersCounter.IsNull)
            {
                await db.StringSetAsync(_ordersCounterKey, 0);
            }
            await db.StringIncrementAsync(_ordersCounterKey, 1);

        }


        public void Probe(ProbeContext context) { }
    }
}
