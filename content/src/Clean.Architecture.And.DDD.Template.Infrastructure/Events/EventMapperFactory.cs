using Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer;
using Clean.Architecture.And.DDD.Template.Domian;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Events
{
    public class EventMapperFactory
    {
        private readonly Dictionary<Type, IEventMapper> _mappers;

        public EventMapperFactory(Dictionary<Type, IEventMapper> mappers)
        {
            _mappers = mappers;
        }

        public IEventMapper GetMapper(IDomainEvent domainEvent)
        {
            if (_mappers.TryGetValue(domainEvent.GetType(), out var mapper))
            {
                return mapper;
            }

            throw new InvalidOperationException($"No mapper found for event type {domainEvent.GetType().Name}");
        }
    }
}
