namespace Clean.Architecture.And.DDD.Template.Domian.Orders.DomainEvents
{
    public record OrderCreatedDomainEvent(OrderId orderId) : IDomainEvent;
}
