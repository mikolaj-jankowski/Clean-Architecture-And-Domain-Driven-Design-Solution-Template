using CA.And.DDD.Template.Domain;
using CA.And.DDD.Template.Infrastructure.Events;
using CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Infrastructure;
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
    public class DomainEventsProcessor : BackgroundService
    {
        private readonly ILogger<DomainEventsProcessor> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private Dictionary<string, System.Reflection.Assembly> _assemblies = new();
        private string conntectionString;

        public DomainEventsProcessor(
            ILogger<DomainEventsProcessor> logger,
            IServiceScopeFactory serviceScopeFactory,
            IDomainEventDispatcher domainEventDispatcher,
             IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _domainEventDispatcher = domainEventDispatcher;
            conntectionString = appSettings.Value.MsSql.ConnectionString;
        }

        /// <summary>
        /// Domain Events aren't being dispatched in the same transaction as saving Aggregates.
        /// Hence they have to be dispatched here as part of eventuall consistency pattern.
        /// </summary>
        /// <param name="state"></param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .ToDictionary(a => a.GetName().Name, a => a);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessDomainEvents(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while executing the worker task.");
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        private async Task ProcessDomainEvents(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<DomainEvent> domainEvents = new List<DomainEvent>();
                using (var connection = new NpgsqlConnection(conntectionString))
                {
                    await connection.OpenAsync(stoppingToken);
                    using (var command = new NpgsqlCommand(@"SELECT ""DomainEventId"", ""OccuredAt"", ""Type"", ""Payload"", ""AssemblyName"" FROM public.""DomainEvent"" WHERE ""ComplatedAt"" IS NULL", connection))
                    {
                        var reader = await command.ExecuteReaderAsync(stoppingToken);

                        while (await reader.ReadAsync(stoppingToken))
                        {
                            var model = new DomainEvent(
                                reader.GetGuid(reader.GetOrdinal("DomainEventId")),
                                reader.GetDateTime(reader.GetOrdinal("OccuredAt")),
                                reader.GetString(reader.GetOrdinal("Type")),
                                reader.GetString(reader.GetOrdinal("AssemblyName")),
                                reader.GetString(reader.GetOrdinal("Payload"))
                                );

                            domainEvents.Add(model);
                        }
                        await reader.CloseAsync();

                        foreach (var domainEvent in domainEvents)
                        {
                            await Publish(domainEvent);

                            using (var updateCommand = new NpgsqlCommand(@"UPDATE public.""DomainEvent"" SET ""ComplatedAt"" = @ComplatedAt WHERE ""DomainEventId"" = @Id", connection))
                            {
                                updateCommand.Parameters.AddWithValue("@ComplatedAt", DateTime.UtcNow);
                                updateCommand.Parameters.AddWithValue("@Id", domainEvent.DomainEventId);

                                int rowsAffected = await updateCommand.ExecuteNonQueryAsync(stoppingToken);
                            }
                        }
                    }
                }

            }
        }

        private async Task Publish(DomainEvent @event)
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
                        await _domainEventDispatcher.Dispatch((IDomainEvent)request);

                    }
                }
            }
        }
    }

}

