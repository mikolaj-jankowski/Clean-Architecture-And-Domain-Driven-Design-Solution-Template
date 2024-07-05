using Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Installers
{
    public static class EntityFrameworkInstaller
    {
        public static void InstallEntityFramework(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=.;Database=Users;User Id=sa;Password=P@ssw0rd123!;TrustServerCertificate=True"));
        }
    }
}
