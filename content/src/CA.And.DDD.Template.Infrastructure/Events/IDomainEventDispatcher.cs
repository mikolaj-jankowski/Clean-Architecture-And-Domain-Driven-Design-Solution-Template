using CA.And.DDD.Template.Domain;

namespace CA.And.DDD.Template.Infrastructure.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
