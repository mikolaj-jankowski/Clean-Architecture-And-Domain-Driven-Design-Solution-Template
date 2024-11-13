using CA.And.DDD.Template.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CA.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Orders
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.OrderItemId);
            builder.Property(x => x.OrderItemId).HasConversion(x => x.Value, v => new OrderItemId(v));
            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.ProductId);
            builder.Property(x => x.ProductName).HasMaxLength(255);
            builder.Property(x => x.Quantity);
            builder.Property(x => x.OrderId).HasConversion(x => x.Value, v => new OrderId(v));
            builder.OwnsOne(x => x.Price, price =>
            {
                price.Property(x => x.Amount).HasPrecision(18,2);
                price.Property(x => x.Currency).HasMaxLength(3);
            });
            builder.Property(x => x.Discount).HasPrecision(18, 2);

        }
    }
}
