using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Installers
{
    public static class EntityFrameworkInstaller
    {
        public static void InstallEntityFramework(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=.;Database=CleanArchitectureAndDDD;User Id=sa;Password=Th3_P@ssw0rd-421;TrustServerCertificate=True"));

        }

        public async static void SeedDatabase(AppDbContext appDbContext)
        {
            try
            {
                appDbContext.Database.Migrate();
            }
            catch (TaskCanceledException ex)
            {
                // Zaloguj błąd
                Console.WriteLine("Task was canceled: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Zaloguj inne błędy
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
