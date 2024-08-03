namespace Clean.Architecture.And.DDD.Template.Application.Shared
{
    public record IntegrationEvent(Guid IntergrationEventId, DateTime OccuredAt, string Type, string Payload, DateTime? PublishedAt = null);
}
