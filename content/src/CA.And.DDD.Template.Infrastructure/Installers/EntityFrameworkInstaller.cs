using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.And.DDD.Template.Infrastructure.Installers
{
    public static class EntityFrameworkInstaller
    {
        public static void InstallEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            if (appSettings != null)
            {
                var msSqlSettings = appSettings.MsSql;
                services.AddDbContext<AppDbContext>(options => options.UseNpgsql(msSqlSettings.ConnectionString));
                services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            }
        }

        public static void SeedDatabase(AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();
        }
    }
}
