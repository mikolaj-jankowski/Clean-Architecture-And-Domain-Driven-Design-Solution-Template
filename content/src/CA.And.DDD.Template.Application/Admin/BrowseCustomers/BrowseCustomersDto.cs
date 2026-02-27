namespace CA.And.DDD.Template.Application.Admin.BrowseCustomers
{
    public sealed record BrowseCustomersDto(List<BrowseCustomerDto> Customers, int TotalCount);
    public sealed record BrowseCustomerDto(Guid Id, string FullName);

}