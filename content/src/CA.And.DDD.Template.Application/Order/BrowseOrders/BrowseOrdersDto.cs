namespace CA.And.DDD.Template.Application.Order.BrowseOrders
{
    public sealed record BrowseOrdersDto(List<BrowseOrderDto> Orders);
    public sealed record BrowseOrderDto(Guid OrderId);
}
