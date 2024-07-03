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
            builder.Services.AddOpenTelemetry()
                //.ConfigureResource(resource => resource.AddService(DiagnosticsConfig.ServiceName))
                .ConfigureResource(resource => resource.AddService("test-mj-final-1", serviceInstanceId: Environment.MachineName))
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation();

                    metrics.AddMeter("CoffeeShop.Api");

                    metrics.AddOtlpExporter(options => options.Endpoint = new Uri("http://localhost:4317"));
                    //metrics.AddConsoleExporter();
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

                    tracing.AddOtlpExporter(options => options.Endpoint = new Uri("http://localhost:4317"));
                    //tracing.AddConsoleExporter();
                });

            builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter(options => options.Endpoint = new Uri("http://localhost:4317")));
            //builder.Logging.AddOpenTelemetry(logging => logging.AddConsoleExporter());

        }

    }
}
