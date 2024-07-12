using Clean.Architecture.And.DDD.Template.Domian.Employees;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
