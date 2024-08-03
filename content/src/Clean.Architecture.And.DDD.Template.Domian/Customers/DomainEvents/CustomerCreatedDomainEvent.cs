namespace Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents
{
    public record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;
}
