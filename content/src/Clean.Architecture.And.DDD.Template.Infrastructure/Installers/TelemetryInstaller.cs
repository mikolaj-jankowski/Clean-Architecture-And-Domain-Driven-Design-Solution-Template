using Clean.Architecture.And.DDD.Template.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Redis;


namespace Clean.Architecture.And.DDD.Template.WebApi.Installers
{
    public static class TelemetryInstaller
    {
        public static void InstallTelemetry(this WebApplicationBuilder builder, IConfiguration configuration, ConnectionMultiplexer redisConnection)
        {
            //var telemetrySettings = builder.Configuration.GetSection(nameof(Telemetry)).Get<Telemetry>();
            var telemetrySettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>().Telemetry;

            builder.Services.AddOpenTelemetry()
                //.ConfigureResource(resource => resource.AddService(DiagnosticsConfig.ServiceName))
                .ConfigureResource(resource => resource.AddService(telemetrySettings.Name, serviceInstanceId: Environment.MachineName))
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation();

                    metrics.AddOtlpExporter(options =>
                    {
                        if(!string.IsNullOrEmpty(telemetrySettings.ExporterUrl))
                        {
                            options.Endpoint = new Uri(telemetrySettings.ExporterUrl);
                        }
                        else
                        {
                            //metrics.AddConsoleExporter();
                        }
                    });

                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRedisInstrumentation(redisConnection, opt => opt.FlushInterval = TimeSpan.FromSeconds(1))
                        .AddEntityFrameworkCoreInstrumentation((options =>
                        {
                            options.EnrichWithIDbCommand = (activity, command) =>
                            {
                                var stateDisplayName = $"{command.CommandType} {command.CommandText} Database: {command.Connection?.Database}";
                                activity.DisplayName = stateDisplayName;
                                activity.SetTag("db.name", stateDisplayName);
                            };
                        }));

                    tracing.AddOtlpExporter(options =>
                    {
                        if(!string.IsNullOrWhiteSpace(telemetrySettings.ExporterUrl))
                        {
                            options.Endpoint = new Uri(telemetrySettings.ExporterUrl);
                        }
                        else
                        {
                            //tracing.AddConsoleExporter();
                        }
                    });
                });

            builder.Logging.AddOpenTelemetry(logging =>
            {
                if (!string.IsNullOrEmpty(telemetrySettings.ExporterUrl))
                {
                    logging.AddOtlpExporter(options => options.Endpoint = new Uri(telemetrySettings.ExporterUrl));
                }
                else
                {
                    //logging.AddConsoleExporter();
                }
            });

        }

    }
}
