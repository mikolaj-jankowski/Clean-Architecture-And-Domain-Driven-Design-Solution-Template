namespace Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents
{
    public record CustomerEmailChnagedDomainEvent(Email OldEmailAddress, Email NewEmailAddress) : IDomainEvent;
}
