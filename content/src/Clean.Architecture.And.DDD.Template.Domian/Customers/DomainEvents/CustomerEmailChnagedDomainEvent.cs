namespace Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents
{
    public record CustomerEmailChnagedDomainEvent(Email oldEmailAddress, Email newEmailAddress) : IDomainEvent;
}
