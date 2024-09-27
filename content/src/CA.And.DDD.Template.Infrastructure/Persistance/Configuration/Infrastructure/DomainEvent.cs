namespace CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Infrastructure
{
    public sealed record DomainEvent(Guid DomainEventId, DateTime OccuredAt, string Type, string Payload, DateTime? ComplatedAt = null);
}
