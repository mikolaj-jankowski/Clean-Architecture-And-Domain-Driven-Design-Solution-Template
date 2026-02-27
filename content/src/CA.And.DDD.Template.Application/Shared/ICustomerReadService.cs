using CA.And.DDD.Template.Application.Customer.GetCustomer;

namespace CA.And.DDD.Template.Application.Shared
{
    public interface ICustomerReadService
    {
        IQueryable<T> ExecuteSqlQueryAsync<T>(string sql, object[] parameters, CancellationToken cancellationToken) where T : class;

        Task<CustomerDto> GetCustomerById(Guid customerId, CancellationToken cancellationToken);
    }
}
