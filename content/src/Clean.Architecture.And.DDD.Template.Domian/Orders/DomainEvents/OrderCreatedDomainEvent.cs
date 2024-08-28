using Clean.Architecture.And.DDD.Template.Domian.Customers;

namespace Clean.Architecture.And.DDD.Template.Domian.Orders.DomainEvents
{
    public record OrderCreatedDomainEvent(Guid OrderId, Guid CustomerId) : IDomainEvent;
}
