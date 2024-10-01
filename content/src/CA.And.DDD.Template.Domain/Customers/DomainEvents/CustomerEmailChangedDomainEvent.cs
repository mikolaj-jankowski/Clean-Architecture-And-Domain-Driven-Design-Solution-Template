namespace CA.And.DDD.Template.Domain.Customers.DomainEvents
{
    public sealed record CustomerEmailChangedDomainEvent(string OldEmailAddress, string NewEmailAddress) : IDomainEvent;
}
