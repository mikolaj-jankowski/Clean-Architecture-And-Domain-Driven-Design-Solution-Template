using CA.And.DDD.Template.Application.Order.BrowseCustomers;

namespace CA.And.DDD.Template.Application.Shared
{
    public interface IAdminReadService
    {
        Task<BrowseCustomersDto> BrowseCustomers(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    }
}
