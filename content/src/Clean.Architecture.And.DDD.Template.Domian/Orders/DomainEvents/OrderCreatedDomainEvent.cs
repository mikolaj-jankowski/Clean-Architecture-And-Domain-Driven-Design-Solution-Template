namespace Clean.Architecture.And.DDD.Template.Domian.Orders.DomainEvents
{
    public sealed record OrderCreatedDomainEvent(Guid OrderId, Guid CustomerId) : IDomainEvent;
}
