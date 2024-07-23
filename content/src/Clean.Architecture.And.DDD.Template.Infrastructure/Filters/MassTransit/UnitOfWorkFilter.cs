using Clean.Architecture.And.DDD.Template.Domian;
using Clean.Architecture.And.DDD.Template.Domian.Employees.DomainEvents;
using Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql;
using Clean.Architecture.And.DDD.Template.Infrastructure.Events;
using MassTransit;
using static Clean.Architecture.And.DDD.Template.Application.Employee.CreateEmployee.CreateEmployeeCommandHandler;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Filters.MassTransit
{
    public class UnitOfWorkFilter<T> : IFilter<ConsumeContext<T>> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IPublishEndpoint _publishEndpoint;

        public UnitOfWorkFilter(AppDbContext appDbContext, IDomainEventDispatcher domainEventDispatcher, IPublishEndpoint publishEndpoint)
        {
            _appDbContext = appDbContext;
            _domainEventDispatcher = domainEventDispatcher;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            await next.Send(context);

            var entities = _appDbContext.ChangeTracker.Entries<Entity>().Where(e => e.Entity.DomainEvents.Any());

            var allEvents = new List<IDomainEvent>();

            foreach(var entity in entities) 
            {
                var events = entity.Entity.DomainEvents.ToList();
                allEvents.AddRange(events);
                entity.Entity.ClearDomainEvents();

                events.ForEach((@event) => _domainEventDispatcher.Dispatch(@event));
            }

            await _appDbContext.SaveChangesAsync();

            //handling itegration events after transaction:
            //https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation
            foreach (var @event in allEvents)
            {
                switch (@event)
                {
                    case EmployeeCreatedDomainEvent:
                        var integrationEvent = new EmployeeCreatedIntegrationEvent();
                        integrationEvent.Text = DateTime.UtcNow.ToShortDateString();
                        await _publishEndpoint.Publish(integrationEvent);
                        break;
                }
            }

        }

        public void Probe(ProbeContext context) { }
    }
}
