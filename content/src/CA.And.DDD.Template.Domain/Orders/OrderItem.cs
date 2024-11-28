namespace CA.And.DDD.Template.Domain.Orders
{
    public class OrderItem
    {
        public OrderItemId OrderItemId { get; private set; }
        public OrderId OrderId { get; private set; }
        public long ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Discount { get; private set; }
        public uint Quantity { get; private set; }

        public Money Price { get; private set; }

        private OrderItem()
        {

        }

        internal static OrderItem Create(long productId, string productName, decimal price, string currency, uint quantity)
        {
            return new OrderItem(productId, productName, price, currency, quantity);
        }

        private OrderItem(long productId, string productName, decimal price, string currency, uint quantity)
        {
            OrderId = new OrderId(Guid.NewGuid());
            OrderItemId = new OrderItemId(Guid.NewGuid());
            ProductId = productId;
            ProductName = productName;
            ProductName = productName;
            Quantity = quantity;
            Price = new Money(price, currency);
            if (quantity == 5)
            {
                Discount = 5.0m;
            }
        }
    }
}
