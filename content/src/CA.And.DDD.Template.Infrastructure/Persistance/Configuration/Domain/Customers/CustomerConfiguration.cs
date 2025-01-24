using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Customers
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);
            builder.Property(x => x.CustomerId).HasConversion(x => x.Value, v => new CustomerId(v));
            builder.Property(x => x.FullName).HasMaxLength(CustomerConstants.Customer.FullNameMaxLength);
            builder.Property(x => x.FullName).HasConversion(x => x.Value, v => new FullName(v));
            builder.Property(x => x.Age)
                .HasConversion(
                    age => age.BirthDate,
                    birthDate => new Age(DateTime.SpecifyKind(birthDate, DateTimeKind.Utc))
                )
                .HasColumnType("timestamp with time zone");
            builder.Property(x => x.Email).HasMaxLength(CustomerConstants.Customer.EmailMaxLength).HasConversion(x => x.Value, v => new Email(v));
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.IsEmailVerified);
            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(x => x.Street).HasMaxLength(CustomerConstants.Customer.StreetMaxLength);
                address.Property(x => x.HouseNumber).HasMaxLength(CustomerConstants.Customer.HouseNumberMaxLength);
                address.Property(x => x.FlatNumber).HasMaxLength(CustomerConstants.Customer.FlatNumberMaxLength);
                address.Property(x => x.Country).HasMaxLength(CustomerConstants.Customer.CountryMaxLength);
                address.Property(x => x.PostalCode).HasMaxLength(CustomerConstants.Customer.PostalCodeMaxLength);
            });
            builder.Property<byte[]>("Version").IsRowVersion();
        }
    }
}
