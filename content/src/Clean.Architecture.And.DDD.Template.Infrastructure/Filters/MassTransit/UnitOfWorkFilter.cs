using Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer;
using Clean.Architecture.And.DDD.Template.Application.Shared;
using Clean.Architecture.And.DDD.Template.Domian;
using Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents;
using Clean.Architecture.And.DDD.Template.Infrastructure.Events;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Infrastructure.DomainEvents;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using System.Text.Json;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Filters.MassTransit
{
    public class UnitOfWorkFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IDateTimeProvider _dateTimeProvider;

        //private readonly IPublishEndpoint _publishEndpoint;

        public UnitOfWorkFilter(AppDbContext appDbContext, IDomainEventDispatcher domainEventDispatcher, IDateTimeProvider dateTimeProvider)
            //IPublishEndpoint publishEndpoint)
        {
            _appDbContext = appDbContext;
            _domainEventDispatcher = domainEventDispatcher;
            _dateTimeProvider = dateTimeProvider;
            //_publishEndpoint = publishEndpoint;
        }
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            await next.Send(context);

            var entities = _appDbContext.ChangeTracker.Entries<Entity>().Where(e => e.Entity.DomainEvents is not null && e.Entity.DomainEvents.Any());

            //var allEvents = new List<IDomainEvent>();

            //foreach(var entity in entities) 
            //{
            //    var events = entity.Entity.DomainEvents.ToList();
            //    allEvents.AddRange(events);
            //    //foreach(var @event in events)
            //    //{
            //    //    await _appDbContext.AddAsync<DomainEvent>(new DomainEvent(Guid.NewGuid(),
            //    //            DateTime.UtcNow,
            //    //            typeof(CustomerCreatedIntegrationEvent).Name,
            //    //            //JsonSerializer.Serialize(@event),
            //    //            "",
            //    //            DateTime.UtcNow));
            //    //}
            //    entity.Entity.ClearDomainEvents();
            //    //events.ForEach((@event) => _domainEventDispatcher.Dispatch(@event));

            //}
            var events = entities.SelectMany(x=>x.Entity.DomainEvents).ToList();
            entities.ToList().ForEach(x => x.Entity.ClearDomainEvents());

            foreach (var entity in events)
            {
                await _appDbContext.AddAsync<DomainEvent>(new DomainEvent(Guid.NewGuid(),
                    _dateTimeProvider.UtcNow,
                    entity.GetType().FullName,
                    JsonSerializer.Serialize(entity)));
            }


            foreach (var @event in events)
            {
                switch (@event)
                {
                    case CustomerCreatedDomainEvent:
                        var e = @event as CustomerCreatedDomainEvent;
                        var customerCreatedIntegrationEvent = new CustomerCreatedIntegrationEvent(e.CustomerId);
                        var integrationEvent = new IntegrationEvent(
                            Guid.NewGuid(),
                            _dateTimeProvider.UtcNow,
                            customerCreatedIntegrationEvent.GetType().FullName,
                            JsonSerializer.Serialize(customerCreatedIntegrationEvent));

                        await _appDbContext.AddAsync<IntegrationEvent>(integrationEvent);
                        break;
                }
            }

            await _appDbContext.SaveChangesAsync();
        }

        public void Probe(ProbeContext context) { }
    }
}
