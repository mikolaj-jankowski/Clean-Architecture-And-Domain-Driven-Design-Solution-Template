using Clean.Architecture.And.DDD.Template.Domian.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Customers
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.CustomerId).HasConversion(x => x.Value, v => new CustomerId(v));
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Surname).HasMaxLength(150);
            builder.Property(x => x.Name).HasConversion(x => x.Name, v => new CustomerName(v));
            builder.Property(x => x.Surname).HasConversion(x => x.Surname, v => new CustomerSurname(v));
        }
    }
}
