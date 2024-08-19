using Clean.Architecture.And.DDD.Template.Domian;
using Clean.Architecture.And.DDD.Template.Infrastructure.Events;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Infrastructure.DomainEvents;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.BackgroundTasks
{
    public class DomainEventsProcessor : BackgroundService
    {
        private readonly ILogger<DomainEventsProcessor> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private Dictionary<string, System.Reflection.Assembly> _assemblies = new();

        public DomainEventsProcessor(
            ILogger<DomainEventsProcessor> logger,
            IServiceScopeFactory serviceScopeFactory,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _domainEventDispatcher = domainEventDispatcher;
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
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var events = context.Set<DomainEvent>().Where(x => x.ComplatedAt == null).ToList();

                foreach (var @event in events)
                {
                    var assembly = _assemblies.SingleOrDefault(assembly => @event.Type.Contains(assembly.Value.GetName().Name));
                    if (assembly is { })
                    {
                        var eventType = assembly.Value.GetType(@event.Type);
                        if (eventType != null)
                        {
                            var request = JsonSerializer.Deserialize(@event.Payload, eventType);

                            if (request != null)
                            {
                                await _domainEventDispatcher.Dispatch((IDomainEvent)request); //TODO: Dispacher?

                                context.Entry(@event).CurrentValues.SetValues(@event with { ComplatedAt = DateTime.UtcNow });
                                context.SaveChanges();
                            }
                        }
                    }

                }
            }
        }
    }

}

