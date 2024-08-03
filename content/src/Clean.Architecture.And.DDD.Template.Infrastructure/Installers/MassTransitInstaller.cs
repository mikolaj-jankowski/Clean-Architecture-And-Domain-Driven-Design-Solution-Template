using Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer;
using Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer.EventHandlers;
using Clean.Architecture.And.DDD.Template.Application.Order.CreateOrder;
using Clean.Architecture.And.DDD.Template.Infrastructure.Filters.MassTransit;
using Clean.Architecture.And.DDD.Template.Infrastructure.Settings;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using static Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer.CreateCustomerCommandHandler;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Installers
{
    public static class MassTransitInstaller
    {
        public static void InstallMassTransit(this WebApplicationBuilder builder)
        {
            var rabbitMqSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!.RabbitMq;

            builder.Services.AddMediator(cfg =>
            {
                //below Consumers for Mediator (in memory)
                cfg.AddConsumer<CustomerCreatedDomainEventHandler>();
                cfg.AddConsumer<CreateOrderCommandHandler>();

                cfg.AddConsumer<CreateCustomerCommandHandler>();
                cfg.ConfigureMediator((context, cfg) =>
                {
                    cfg.UseConsumeFilter(typeof(UnitOfWorkFilter<>), context);
                    cfg.UseMessageRetry(x => x.Interval(3, TimeSpan.FromSeconds(15)));
                });
            });

            builder.Services.AddMassTransit(x =>
            {
                //below Consumers for RabbitMq
                x.AddConsumer<CustomerCreatedIntegrationEventHandler>();

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.Host);

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }

}
