using CA.And.DDD.Template.Application.Customer.Shared;

namespace CA.And.DDD.Template.Application.Shared
{
    public interface ICustomerReadService
    {
        IQueryable<T> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken) where T : class;

        Task<CustomerDto> GetCustomerByEamil(string email, CancellationToken cancellationToken);
    }
}
