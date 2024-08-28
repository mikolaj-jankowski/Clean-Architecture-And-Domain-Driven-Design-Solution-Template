namespace Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents
{
    public sealed record CustomerEmailChnagedDomainEvent(string OldEmailAddress, string NewEmailAddress) : IDomainEvent;
}
