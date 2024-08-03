namespace Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Infrastructure.DomainEvents
{
    public record DomainEvent(Guid DomainEventId, DateTime OccuredAt, string Type, string Payload, DateTime? ComplatedAt = null);
}
