namespace Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents
{
    public record CustomerEmailVerifiedDomainEvent(Email NewEmailAddress) : IDomainEvent;
}
