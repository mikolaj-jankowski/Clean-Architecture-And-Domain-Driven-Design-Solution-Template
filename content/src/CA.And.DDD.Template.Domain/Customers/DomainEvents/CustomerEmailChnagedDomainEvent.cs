namespace CA.And.DDD.Template.Domain.Customers.DomainEvents
{
    public sealed record CustomerEmailChnagedDomainEvent(string OldEmailAddress, string NewEmailAddress) : IDomainEvent;
}
