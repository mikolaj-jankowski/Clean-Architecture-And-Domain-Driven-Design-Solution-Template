namespace Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents
{
    public sealed record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;
}
