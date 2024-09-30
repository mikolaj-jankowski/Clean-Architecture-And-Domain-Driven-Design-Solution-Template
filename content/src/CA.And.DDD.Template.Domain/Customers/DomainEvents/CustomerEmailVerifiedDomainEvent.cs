namespace CA.And.DDD.Template.Domain.Customers.DomainEvents
{
    public sealed record CustomerEmailVerifiedDomainEvent(string NewEmailAddress) : IDomainEvent;
}
