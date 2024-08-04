using Clean.Architecture.And.DDD.Template.Domian.Customers;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.MsSql
{
    public class AppDbContext : DbContext
    {
        //public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
