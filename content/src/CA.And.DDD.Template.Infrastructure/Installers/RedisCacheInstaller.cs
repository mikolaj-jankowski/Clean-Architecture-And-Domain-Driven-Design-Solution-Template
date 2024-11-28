using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.And.DDD.Template.Infrastructure.Installers
{
    public static class RedisCacheInstaller
    {
        public static void InstallRedisCache(this WebApplicationBuilder builder)
        {
            var redisSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!.Redis;
            builder.Services.AddStackExchangeRedisCache(action =>
            {
                var connection = $"{redisSettings.Host}:{redisSettings.Port},password={redisSettings.Password}";
                action.Configuration = connection;
            });

        }
    }
}
