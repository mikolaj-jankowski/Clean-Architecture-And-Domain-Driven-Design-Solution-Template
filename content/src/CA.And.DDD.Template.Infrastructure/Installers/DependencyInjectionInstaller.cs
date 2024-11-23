using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain;
using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Domain.Customers.DomainEvents;
using CA.And.DDD.Template.Domain.Orders;
using CA.And.DDD.Template.Infrastructure.BackgroundTasks;
using CA.And.DDD.Template.Infrastructure.Events;
using CA.And.DDD.Template.Infrastructure.Exceptions;
using CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Customers;
using CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Orders;
using CA.And.DDD.Template.Infrastructure.ReadServices;
using CA.And.DDD.Template.Infrastructure.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CA.And.DDD.Template.Infrastructure.Installers
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
            builder.Services.AddValidatorsFromAssemblyContaining<IApplicationValidator>(ServiceLifetime.Transient);
            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<CommandValidationExceptionHandler>();
            builder.Services.AddSingleton<ICacheService, CacheService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IEmailTemplateFactory, EmailTemplateFactory>();
            builder.Services.AddScoped<OrderDomainService>();
            builder.Services.AddScoped<IOrderReadService, OrderReadService>();
            builder.Services.AddScoped<ICustomerReadService, CustomerReadService>();

        }

    }
}
