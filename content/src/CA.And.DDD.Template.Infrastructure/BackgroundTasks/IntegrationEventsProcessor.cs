using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Infrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Text.Json;

namespace CA.And.DDD.Template.Infrastructure.BackgroundTasks
{
    public class IntegrationEventsProcessor : BackgroundService
    {
        private readonly ILogger<IntegrationEventsProcessor> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Dictionary<string, System.Reflection.Assembly> _assemblies = new();
        private IPublishEndpoint _publishEndpoint;
        private string conntectionString;

        public IntegrationEventsProcessor(ILogger<IntegrationEventsProcessor> logger, IServiceScopeFactory serviceScopeFactory, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _publishEndpoint = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IPublishEndpoint>();
            conntectionString = appSettings.Value.MsSql.ConnectionString;

        }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .ToDictionary(a => a.GetName().Name, a => a);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessIntegrationEvents(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while executing the worker task.");
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        private async Task ProcessIntegrationEvents(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<IntegrationEvent> integrationEvents = new List<IntegrationEvent>();
                using (var connection = new NpgsqlConnection(conntectionString))
                {
                    await connection.OpenAsync(stoppingToken);
                    using (var command = new NpgsqlCommand(@"SELECT ""IntergrationEventId"", ""OccuredAt"", ""Type"", ""Payload"", ""AssemblyName"" FROM public.""IntegrationEvent"" WHERE ""PublishedAt"" IS NULL", connection))
                    {
                        var reader = await command.ExecuteReaderAsync(stoppingToken);

                        while (await reader.ReadAsync(stoppingToken))
                        {
                            var model = new IntegrationEvent(
                                reader.GetGuid(reader.GetOrdinal("IntergrationEventId")),
                                reader.GetDateTime(reader.GetOrdinal("OccuredAt")),
                                reader.GetString(reader.GetOrdinal("Type")),
                                reader.GetString(reader.GetOrdinal("AssemblyName")),
                                reader.GetString(reader.GetOrdinal("Payload"))
                                );

                            integrationEvents.Add(model);
                        }
                        await reader.CloseAsync();

                        foreach (var integrationEvent in integrationEvents)
                        {
                            await Publish(integrationEvent);

                            using (var updateCommand = new NpgsqlCommand(@"UPDATE public.""IntegrationEvent"" SET ""PublishedAt"" = @PublishedAt WHERE ""IntergrationEventId"" = @Id", connection))
                            {
                                updateCommand.Parameters.AddWithValue("@PublishedAt", DateTime.UtcNow);
                                updateCommand.Parameters.AddWithValue("@Id", integrationEvent.IntergrationEventId);

                                int rowsAffected = await updateCommand.ExecuteNonQueryAsync(stoppingToken);
                            }
                        }
                    }
                    await connection.CloseAsync();
                }
            }
        }


        private async Task Publish(IntegrationEvent @event)
        {
            var assembly = _assemblies.SingleOrDefault(assembly => assembly.Value.GetName().Name == @event.AssemblyName);
            if (assembly is { })
            {
                var eventType = assembly.Value.GetType(@event.Type);
                if (eventType != null)
                {
                    var request = JsonSerializer.Deserialize(@event.Payload, eventType);

                    if (request != null)
                    {
                        await _publishEndpoint.Publish(request);

                    }
                }
            }
        }
    }

}
