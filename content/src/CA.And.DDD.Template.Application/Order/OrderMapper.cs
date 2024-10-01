using CA.And.DDD.Template.Domain.Orders;

namespace CA.And.DDD.Template.Application.Order
{
    public sealed record OrderItemDto(Guid OrderItemId, string productName, decimal price);
    public static class OrderQueryResponseMapper
    {
        public static List<OrderItemDto> MapToOrderItemDto(this IReadOnlyCollection<OrderItem> orderItems)
        {
            return orderItems.Select(x => new OrderItemDto(x.OrderItemId.Value, x.ProductName, x.Price.Amount)).ToList();
        }
    }
}
