namespace Clean.Architecture.And.DDD.Template.Domian.Orders.DomainEvents
{
    public record OrderPlacedDomainEvent(OrderId orderId) : IDomainEvent;
}
