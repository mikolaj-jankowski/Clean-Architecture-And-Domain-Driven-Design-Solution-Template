using CA.And.DDD.Template.Application.Order.BrowseOrders;

namespace CA.And.DDD.Template.Application.Order.BrowseCustomers
{
    public sealed record BrowseCustomersDto(List<BrowseCustomerDto> Customers, int TotalCount);
    public sealed record BrowseCustomerDto(Guid Id, string FullName);

}

