using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Domain.Orders.DomainEvents;
using CA.And.DDD.Template.Domain.Orders.Exceptions;
using CA.And.DDD.Template.Domain.Orders.Policies;

namespace CA.And.DDD.Template.Domain.Orders
{
    public class Order : Entity
    {
        public OrderId OrderId { get; private set; } 
        public ShippingAddress ShippingAddress { get; private set; }
        public CustomerId CustomerId { get; private set; }

        public DateTime OrderDate { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Money TotalAmount { get; private set; }

        private Order()
        {

        }

        public static Order Create(
            CustomerId customerId,
            ShippingAddress shippingAddress,
            DateTime orderDate)
        {
            return new Order(customerId, shippingAddress, orderDate);
        }



        private Order(CustomerId customerId, ShippingAddress shippingAddress, DateTime orderDate)
        {
            CustomerId = customerId;
            OrderId = new OrderId(Guid.NewGuid());
            ShippingAddress = shippingAddress;
            OrderDate = orderDate;

            _orderItems = new List<OrderItem>();

            AddDomainEvent(new OrderCreatedDomainEvent(this.OrderId.Value, this.CustomerId.Value));
        }
        internal decimal ApplyDiscount(Money totalSpentMoneyInLast31Days)
        {
            var discount = new AmountBasedDiscountPolicy().CalculateDiscount(totalSpentMoneyInLast31Days, _orderItems);
            return discount;
        }

        public void AddOrderItem(long productId, string productName, decimal price, string currency, uint quantity = 1)
        {
            if (quantity > 5)
            {
                throw new MaximumQuantityExceededDomainException();
            }

            var orderItem = OrderItem.Create(productId, productName, price, currency, quantity);
            _orderItems.Add(orderItem);
        }
    }
}
