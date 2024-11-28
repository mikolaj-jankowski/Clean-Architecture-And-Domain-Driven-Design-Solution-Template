using CA.And.DDD.Template.Domain;

namespace CA.And.DDD.Template.Infrastructure.Events
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

            return null;
        }
    }
}
