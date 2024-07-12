using Clean.Architecture.And.DDD.Template.Application.Employee.CreateEmployee;
using Clean.Architecture.And.DDD.Template.Infrastructure.Filters.MassTransit;
using MassTransit;
using Microsoft.AspNetCore.Builder;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Installers
{
    public static class MassTransitInstaller
    {
        public static void InstallMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediator(cfg =>
            {
                cfg.AddConsumer<CreateEmployeeCommandHandler>();
                cfg.ConfigureMediator((context, cfg) =>
                {
                    cfg.UseConsumeFilter(typeof(UnitOfWorkFilter<>), context);
                });
            });
        }
    }
}
