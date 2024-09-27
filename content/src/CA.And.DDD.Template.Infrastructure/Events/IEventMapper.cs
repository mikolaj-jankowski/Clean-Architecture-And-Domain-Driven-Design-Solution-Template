using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domian;

namespace CA.And.DDD.Template.Infrastructure.Events
{
    public interface IEventMapper
    {
        IntegrationEvent Map(IDomainEvent domainEvent);
    }
}
