using Clean.Architecture.And.DDD.Template.Domian.Customers;
using Clean.Architecture.And.DDD.Template.Domian.Orders.DomainEvents;
using Clean.Architecture.And.DDD.Template.Domian.Orders.Exceptions;

namespace Clean.Architecture.And.DDD.Template.Domian.Orders
{
    public class Order : Entity
    {
        public OrderId OrderId { get; private set; } //UUID v7 in .NET 9
        public ShippingAddress ShippingAddress { get; private set; }
        public CustomerId CustomerId { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public static Order Create(Guid customerId, string streetName, string postalCode)
        {
            return new Order(customerId, streetName, postalCode);
        }

        private Order(Guid customerId, string streetName, string postalCode)
        {
            CustomerId = new CustomerId(customerId);
            OrderId = new OrderId(Guid.NewGuid());//Guid.CreateVersion7(DateTimeOffset.UtcNow);
            ShippingAddress = new ShippingAddress(streetName, postalCode);

            AddDomainEvent(new OrderCreatedDomainEvent(this.OrderId));
        }

        public void AddOrderItem(long productId, int quantity = 1)
        {
            if (quantity > 5)
            {
                throw new MaximumQuantityExceededDomainException();
            }

            var orderItem = OrderItem.Create(productId, quantity);
            _orderItems.Add(orderItem);
        }
    }
}
