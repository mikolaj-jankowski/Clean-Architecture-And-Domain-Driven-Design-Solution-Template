using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.And.DDD.Template.Infrastructure.Installers
{
    public static class SettingsInstaller
    {
        public static void InstallApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        }
    }

}
