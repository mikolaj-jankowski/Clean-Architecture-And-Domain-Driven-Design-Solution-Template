namespace CA.And.DDD.Template.Application.Order.BrowseOrders
{
    public sealed record BrowseOrdersDto(List<BrowseOrderDto> Orders, int TotalCount);
    public sealed record BrowseOrderDto(Guid OrderId, ICollection<BrowseProductDto> Product, decimal OrderTotal);
    public sealed record BrowseProductDto(string ProductName, uint Quantity);

}
