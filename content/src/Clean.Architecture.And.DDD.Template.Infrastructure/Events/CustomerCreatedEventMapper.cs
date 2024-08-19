using Clean.Architecture.And.DDD.Template.Application.Shared;
using Clean.Architecture.And.DDD.Template.Domian;
using Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents;
using Newtonsoft.Json;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Events
{
    public class CustomerCreatedEventMapper : IEventMapper
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CustomerCreatedEventMapper(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public IntegrationEvent Map(IDomainEvent domainEvent)
        {
            var customerCreatedIntegrationEvent = domainEvent as CustomerCreatedDomainEvent;
            var integrationEvent = new IntegrationEvent(
                Guid.NewGuid(),
                _dateTimeProvider.UtcNow,
                customerCreatedIntegrationEvent.GetType().FullName,
                JsonConvert.SerializeObject(customerCreatedIntegrationEvent, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }));
            return integrationEvent;
        }
    }
}
