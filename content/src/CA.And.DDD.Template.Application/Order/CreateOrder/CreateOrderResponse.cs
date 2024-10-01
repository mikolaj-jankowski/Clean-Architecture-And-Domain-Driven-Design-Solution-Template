using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Domain.Orders;

namespace CA.And.DDD.Template.Application.Order.CreateOrder
{
    public sealed record CreateOrderResponse(Guid OrderId, List<OrderItemDto> orderItems);
}
