using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain;
using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Domain.Customers.DomainEvents;
using CA.And.DDD.Template.Domain.Orders;
using CA.And.DDD.Template.Infrastructure.Authentication;
using CA.And.DDD.Template.Infrastructure.BackgroundTasks;
using CA.And.DDD.Template.Infrastructure.Events;
using CA.And.DDD.Template.Infrastructure.Exceptions;
using CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Customers;
using CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Orders;
using CA.And.DDD.Template.Infrastructure.ReadServices;
using CA.And.DDD.Template.Infrastructure.Shared;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.And.DDD.Template.Infrastructure.Installers
{
    public static class DependencyInjectionInstaller
    {
        public static void InstallDependencyInjectionRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddHostedService<DomainEventsProcessor>();
            services.AddHostedService<IntegrationEventsProcessor>();
            
            services.AddTransient<CustomerCreatedEventMapper>();
            services.AddSingleton<EventMapperFactory>(provider =>
            {
                var mappers = new Dictionary<Type, IEventMapper>
                {
                    { typeof(CustomerCreatedDomainEvent), provider.GetRequiredService<CustomerCreatedEventMapper>() },
                };

                return new EventMapperFactory(mappers);
            });
            services.AddValidatorsFromAssemblyContaining<IApplicationValidator>(ServiceLifetime.Transient);
            services.AddProblemDetails();
            services.AddExceptionHandler<CommandValidationExceptionHandler>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailTemplateFactory, EmailTemplateFactory>();
            services.AddScoped<OrderDomainService>();
            services.AddScoped<IOrderReadService, OrderReadService>();
            services.AddScoped<ICustomerReadService, CustomerReadService>();
            services.AddScoped<IAdminReadService, AdminReadService>();
            services.AddHttpClient<IAuthenticationService, KeycloakAuthenticationService>();

        }

    }
}
