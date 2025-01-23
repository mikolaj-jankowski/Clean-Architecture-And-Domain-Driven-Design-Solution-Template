using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CA.And.DDD.Template.Infrastructure.Installers
{
    public static class CorsInstaller
    {
        public static string DefaultCorsPolicyName = "AllowSpecificOrigins";
        public static void InstallCors(this WebApplicationBuilder builder)
        {
            var cors = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!.Cors;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName,
                    corsBuilder =>
                    {
                        corsBuilder.WithOrigins(cors.AllowedOrigins)
                         .WithMethods(cors.AllowedMethods)
                         .WithHeaders(cors.AllowedHeaders);
                    });
            });

        }
    }
}
