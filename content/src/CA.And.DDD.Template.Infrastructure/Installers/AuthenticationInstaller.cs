using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace CA.And.DDD.Template.Infrastructure.Installers
{
    public static class AuthenticationInstaller
    {
        public static void InstallAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authentication = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!.Authentication;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = authentication.Authority;
                    options.Audience = authentication.Audience;
                    options.RequireHttpsMetadata = false; //TODO: set to true on production env
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var claimsIdentity = context.Principal.Identity as System.Security.Claims.ClaimsIdentity;
                            if (claimsIdentity == null)
                                return Task.CompletedTask;

                            var resourceAccessClaim = claimsIdentity.FindFirst("resource_access")?.Value;
                            if (!string.IsNullOrEmpty(resourceAccessClaim))
                            {
                                var resourceAccess = JsonSerializer.Deserialize<JsonElement>(resourceAccessClaim);

                                if (resourceAccess.TryGetProperty(authentication.ClientId, out var mykidClientRoles))
                                {
                                    if (mykidClientRoles.TryGetProperty("roles", out var roles))
                                    {
                                        foreach (var role in roles.EnumerateArray())
                                        {
                                            claimsIdentity.AddClaim(new System.Security.Claims.Claim("Role", role.GetString()));
                                        }
                                    }
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireClaim("Role", "admin-role"));
            });
        }
    }
}
