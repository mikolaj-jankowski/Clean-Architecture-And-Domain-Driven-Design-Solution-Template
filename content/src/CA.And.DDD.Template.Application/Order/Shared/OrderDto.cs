using CA.And.DDD.Template.Domain.Orders;

namespace CA.And.DDD.Template.Application.Order.Shared
{
    public sealed record OrderDto(Guid OrderId, List<OrderItemDto> OrderItems);

    public sealed record OrderItemDto(Guid OrderItemId, string productName, decimal price);

    public static class OrderMapper
    {
        public static OrderDto ToDto(this CA.And.DDD.Template.Domain.Orders.Order order)
        {
            return new OrderDto(order.OrderId.Value, order.OrderItems.ToDto());
        }
        public static List<OrderItemDto> ToDto(this IReadOnlyCollection<OrderItem> orderItems)
        {
            return orderItems.Select(x => new OrderItemDto(x.OrderItemId.Value, x.ProductName, x.Price.Amount)).ToList();
        }
    }
}
