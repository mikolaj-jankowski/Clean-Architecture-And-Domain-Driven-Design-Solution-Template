namespace CA.And.DDD.Template.Domain.Orders.DomainEvents
{
    public sealed record OrderCreatedDomainEvent(Guid OrderId, Guid CustomerId) : IDomainEvent;
}
