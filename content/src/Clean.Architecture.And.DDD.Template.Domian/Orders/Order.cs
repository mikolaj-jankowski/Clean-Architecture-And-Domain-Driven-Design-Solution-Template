﻿using Clean.Architecture.And.DDD.Template.Domian.Customers;
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

        }

        public static Order Create(CustomerId customerId, ShippingAddress shippingAddress)
        {
            return new Order(customerId, shippingAddress);
        }

        private Order(CustomerId customerId, ShippingAddress shippingAddress)
        {
            CustomerId = customerId;
            OrderId = new OrderId(Guid.NewGuid());//Guid.CreateVersion7(DateTimeOffset.UtcNow);
            ShippingAddress = shippingAddress;

            _orderItems = new List<OrderItem>();

            AddDomainEvent(new OrderCreatedDomainEvent(this.OrderId.Value, this.CustomerId.Value));
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