namespace CA.And.DDD.Template.Domain.Customers.DomainEvents
{
    public sealed record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;
}
