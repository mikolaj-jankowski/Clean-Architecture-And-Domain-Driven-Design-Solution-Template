using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.And.DDD.Template.Infrastructure.Installers
{
    public static class RedisCacheInstaller
    {
        public static void InstallRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!.Redis;
            services.AddStackExchangeRedisCache(action =>
            {
                var connection = $"{redisSettings.Host}:{redisSettings.Port},password={redisSettings.Password}";
                action.Configuration = connection;
            });

        }
    }
}
