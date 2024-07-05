using Clean.Architecture.And.DDD.Template.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Installers
{
    public static class SettingsInstaller
    {
        public static void InstallApplicationSettings(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
        }
    }

}
