namespace CA.And.DDD.Template.Infrastructure.Settings
{
    public record Cors
    {
        public string[] AllowedOrigins { get; init; } = Array.Empty<string>();
        public string[] AllowedMethods { get; init; } = Array.Empty<string>();
        public string[] AllowedHeaders { get; init; } = Array.Empty<string>();
    }
}
