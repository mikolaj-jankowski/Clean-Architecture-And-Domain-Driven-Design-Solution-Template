using CA.And.DDD.Template.Application.Admin.BrowseCustomers;

namespace CA.And.DDD.Template.Application.Shared
{
    public interface IAdminReadService
    {
        Task<BrowseCustomersDto> BrowseCustomers(PaginationParameters paginationParameters, CancellationToken cancellationToken);
    }
}
