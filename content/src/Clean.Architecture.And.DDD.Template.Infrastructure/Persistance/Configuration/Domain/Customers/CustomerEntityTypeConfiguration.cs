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
            builder.Property(x => x.FullName).HasMaxLength(50);
            builder.Property(x => x.FullName).HasConversion(x => x.Value, v => new FullName(v));
            builder.Property(x => x.Age).HasConversion(x => x.BirthDate, v => new Age(v));
            builder.Property<Email>(x => x.Email).HasConversion(x => x.Value, v => new Email(v));
            builder.Property(x => x.IsEmailVerified);
        }
    }
}
