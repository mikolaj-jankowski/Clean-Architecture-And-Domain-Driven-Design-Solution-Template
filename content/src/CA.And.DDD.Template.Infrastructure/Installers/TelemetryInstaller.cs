using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Redis;


namespace CA.And.DDD.Template.Infrastructure.Installers
{
    public static class TelemetryInstaller
    {
        public static void InstallTelemetry(this IServiceCollection services, IConfiguration configuration, ConnectionMultiplexer redisConnection, ILoggingBuilder loggingBuilder)
        {
            var telemetrySettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>().Telemetry;
            var url = $"{telemetrySettings.Host}:{telemetrySettings.Port}";

            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(telemetrySettings.Name, serviceInstanceId: Environment.MachineName))
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation();

                    metrics.AddOtlpExporter(options =>
                    {
                        if (!string.IsNullOrEmpty(url))
                        {
                            options.Endpoint = new Uri(url);
                        }
                    });

                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRedisInstrumentation(redisConnection, opt => opt.FlushInterval = TimeSpan.FromSeconds(1))
                        .AddEntityFrameworkCoreInstrumentation(options =>
                        {
                            options.EnrichWithIDbCommand = (activity, command) =>
                            {
                                var stateDisplayName = $"{command.CommandType} {command.CommandText} Database: {command.Connection?.Database}";
                                activity.DisplayName = stateDisplayName;
                                activity.SetTag("db.name", stateDisplayName);
                            };
                        });

                    tracing.AddOtlpExporter(options =>
                    {
                        if (!string.IsNullOrWhiteSpace(url))
                        {
                            options.Endpoint = new Uri(url);
                        }
                    });
                });

            loggingBuilder.AddOpenTelemetry(logging =>
            {
                if (!string.IsNullOrEmpty(url))
                {
                    logging.AddOtlpExporter(options => options.Endpoint = new Uri(url));
                }
            });

        }
    }
}
