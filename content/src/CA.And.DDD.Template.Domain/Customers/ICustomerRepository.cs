namespace CA.And.DDD.Template.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer?> GetAsync(string email, CancellationToken cancellationToken = default);
        Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
    }
}
