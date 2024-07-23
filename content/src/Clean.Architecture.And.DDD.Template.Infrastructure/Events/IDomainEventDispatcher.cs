using Clean.Architecture.And.DDD.Template.Domian;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }
}
