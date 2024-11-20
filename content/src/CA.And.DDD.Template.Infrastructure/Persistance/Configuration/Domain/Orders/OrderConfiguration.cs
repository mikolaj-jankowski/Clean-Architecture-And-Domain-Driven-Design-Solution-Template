using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Orders
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.OrderId).HasConversion(x => x.Value, v => new OrderId(v));
            builder.OwnsOne(x => x.TotalAmount, price =>
            {
                price.Property(x => x.Amount).HasPrecision(18, 2);
                price.Property(x => x.Currency).HasMaxLength(OrderConstants.Order.CurrencyMaxLength);
            });
            builder.OwnsOne(x => x.Discount, price =>
            {
                price.Property(x => x.Amount).HasPrecision(18, 2);
                price.Property(x => x.Type).HasConversion<string>().HasMaxLength(128);
            });
            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId);
            builder.HasMany(x => x.OrderItems).WithOne().HasForeignKey(x => x.OrderId);
            builder.OwnsOne(x => x.ShippingAddress, shippingAddresBuilder =>
            {
                shippingAddresBuilder.Property(x => x.PostalCode).HasMaxLength(OrderConstants.Order.PostalCodeMaxLength);
                shippingAddresBuilder.Property(x => x.Street).HasMaxLength(OrderConstants.Order.StreetMaxLength);
            });
            builder.Property(x => x.OrderDate);
            builder.Property<byte[]>("Version").IsRowVersion();
        }
    }
}
