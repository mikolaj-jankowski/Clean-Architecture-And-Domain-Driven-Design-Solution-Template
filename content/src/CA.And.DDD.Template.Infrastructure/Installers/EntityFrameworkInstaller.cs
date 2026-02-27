using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.And.DDD.Template.Infrastructure.Installers
{
    public static class EntityFrameworkInstaller
    {
        public static void InstallEntityFramework(this WebApplicationBuilder builder)
        {
            var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            if (appSettings != null)
            {
                var msSqlSettings = appSettings.MsSql;
                builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(msSqlSettings.ConnectionString));
                builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            }
        }

        public static void SeedDatabase(AppDbContext appDbContext)
        {
            appDbContext.Database.Migrate();
        }
    }
}
