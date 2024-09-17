using Clean.Architecture.And.DDD.Template.Domian;
using Clean.Architecture.And.DDD.Template.Domian.Customers;
using Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents;
using Clean.Architecture.And.DDD.Template.Domian.Orders;
using Clean.Architecture.And.DDD.Template.Infrastructure.BackgroundTasks;
using Clean.Architecture.And.DDD.Template.Infrastructure.Events;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Customers;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Orders;
using Clean.Architecture.And.DDD.Template.Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Clean.Architecture.And.DDD.Template.Infrastructure.Installers
{
    public static class DependencyInjectionInstaller
    {
        public static void InstallDependencyInjectionRegistrations(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            builder.Services.AddHostedService<DomainEventsProcessor>();
            builder.Services.AddHostedService<IntegrationEventsProcessor>();

            builder.Services.AddTransient<CustomerCreatedEventMapper>();
            builder.Services.AddSingleton<EventMapperFactory>(provider =>
            {
                var mappers = new Dictionary<Type, IEventMapper>
                {
                    { typeof(CustomerCreatedDomainEvent), provider.GetRequiredService<CustomerCreatedEventMapper>() },
                };

                return new EventMapperFactory(mappers);
            });
        }

    }
}
