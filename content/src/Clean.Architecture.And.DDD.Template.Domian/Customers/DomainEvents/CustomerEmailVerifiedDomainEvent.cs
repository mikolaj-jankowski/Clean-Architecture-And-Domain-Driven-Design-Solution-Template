namespace Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents
{
    public sealed record CustomerEmailVerifiedDomainEvent(string NewEmailAddress) : IDomainEvent;
}
