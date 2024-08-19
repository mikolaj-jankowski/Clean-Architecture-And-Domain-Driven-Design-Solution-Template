using Clean.Architecture.And.DDD.Template.Application.Shared;
using Clean.Architecture.And.DDD.Template.Domian;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Events
{
    public interface IEventMapper
    {
        IntegrationEvent Map(IDomainEvent domainEvent);
    }
}
