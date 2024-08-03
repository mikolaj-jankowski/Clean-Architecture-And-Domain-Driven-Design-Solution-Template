using Clean.Architecture.And.DDD.Template.Domian.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Orders
{
    internal class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.OrderItemId);
            builder.Property(x => x.OrderItemId).HasConversion(x => x.Value, v => new OrderItemId(v));
            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.OrderId).HasConversion(x => x.Value, v => new OrderId(v));
            builder.OwnsOne(x => x.Price);
        }
    }
}
