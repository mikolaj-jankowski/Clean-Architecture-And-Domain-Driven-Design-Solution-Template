namespace CA.And.DDD.Template.Infrastructure.Settings
{
    public record AppSettings
    {
        public required Redis Redis { get; init; }
        public required Telemetry Telemetry { get; init; }
        public required RabbitMq RabbitMq { get; init; }
        public required MsSql MsSql { get; init; }
        public required Cache Cache { get; init; }
        public required Smtp Smtp { get; init; }
        public required Authentication Authentication { get; init; }
        public required Cors Cors { get; init; }
    }
}
