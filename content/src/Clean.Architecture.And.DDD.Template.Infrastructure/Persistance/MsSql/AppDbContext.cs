using Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql.Models;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
