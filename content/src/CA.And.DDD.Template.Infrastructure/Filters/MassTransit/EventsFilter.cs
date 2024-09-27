using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domian;
using CA.And.DDD.Template.Infrastructure.Events;
using CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Infrastructure;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using Newtonsoft.Json;

namespace CA.And.DDD.Template.Infrastructure.Filters.MassTransit
{
    public class EventsFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly EventMapperFactory _mapperFactory;

        public EventsFilter(
            AppDbContext appDbContext,
            IDateTimeProvider dateTimeProvider,
            EventMapperFactory mapperFactory)
        {
            _appDbContext = appDbContext;
            _dateTimeProvider = dateTimeProvider;
            _mapperFactory = mapperFactory;
        }
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            await next.Send(context);

            var entities = _appDbContext.ChangeTracker.Entries<Entity>().Where(e => e.Entity.DomainEvents is not null && e.Entity.DomainEvents.Any());

            var events = entities.SelectMany(x=>x.Entity.DomainEvents).ToList();
            entities.ToList().ForEach(x => x.Entity.ClearDomainEvents());

            foreach (var domainEvent in events)
            {

                await _appDbContext.AddAsync<DomainEvent>(
                    new DomainEvent(
                    Guid.NewGuid(),
                    _dateTimeProvider.UtcNow,
                    domainEvent.GetType().FullName,
                    JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All })));
            }

            await _appDbContext.AddRangeAsync(RemapToIntegrationEvents(events));

            await _appDbContext.SaveChangesAsync();
        }

        public void Probe(ProbeContext context) { }


        //TODO: comment
        public List<IntegrationEvent> RemapToIntegrationEvents(List<IDomainEvent> domainEvents)
        {
            List<IntegrationEvent> integrationEvents = new List<IntegrationEvent>();
            foreach (var domainEvent in domainEvents)
            {
                var intergrationEvent = _mapperFactory
                    .GetMapper(domainEvent)
                    ?.Map(domainEvent);
                if(intergrationEvent != null)
                {
                    integrationEvents.Add(intergrationEvent);
                }
                
            }
            return integrationEvents;
        }
    }

    
}
