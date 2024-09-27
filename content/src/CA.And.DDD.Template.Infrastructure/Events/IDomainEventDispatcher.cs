using CA.And.DDD.Template.Domian;

namespace CA.And.DDD.Template.Infrastructure.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
