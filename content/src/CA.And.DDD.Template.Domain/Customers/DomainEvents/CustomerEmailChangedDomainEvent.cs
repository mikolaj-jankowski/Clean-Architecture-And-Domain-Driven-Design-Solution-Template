namespace CA.And.DDD.Template.Domain.Customers.DomainEvents
{
    public sealed record CustomerEmailChangedDomainEvent(Guid CustomerId, string OldEmailAddress, string NewEmailAddress) : IDomainEvent;
}
