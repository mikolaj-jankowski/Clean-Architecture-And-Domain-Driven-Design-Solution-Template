using Clean.Architecture.And.DDD.Template.Domian.Customers;

namespace Clean.Architecture.And.DDD.Template.Domian.Orders.DomainEvents
{
    //public record OrderCreatedDomainEvent(OrderId OrderId, CustomerId CustomerId) : IDomainEvent;
    //public record OrderCreatedDomainEvent(Guid OrderId, Guid CustomerId) : IDomainEvent;

    public class OrderCreatedDomainEvent : IDomainEvent
    {
        public OrderCreatedDomainEvent(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }

        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
