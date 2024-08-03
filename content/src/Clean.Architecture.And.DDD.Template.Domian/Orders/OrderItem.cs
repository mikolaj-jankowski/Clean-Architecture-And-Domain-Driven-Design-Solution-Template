using System.Data.SqlTypes;

namespace Clean.Architecture.And.DDD.Template.Domian.Orders
{
    public class OrderItem
    {
        public OrderItemId OrderItemId { get; private set; } //UUID v7 in .NET 9
        public OrderId OrderId { get; private set; } //UUID v7 in .NET 9
        public long ProductId { get; private set; }
        public decimal Discount { get; private set; }

        public Money Price { get; private set; }

        private OrderItem()
        {

        }

        public static OrderItem Create(long productId, int quantity)
        {
            return new OrderItem(productId, quantity);
        }

        private OrderItem(long productId, int quantity)
        {
            OrderId = new OrderId(Guid.NewGuid());
            ProductId = productId;
            if (quantity == 5)
            {
                Discount = 5.0m;
            }
        }
    }
}
