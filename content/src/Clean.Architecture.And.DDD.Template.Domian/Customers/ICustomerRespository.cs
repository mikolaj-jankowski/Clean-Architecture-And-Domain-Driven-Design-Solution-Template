namespace Clean.Architecture.And.DDD.Template.Domian.Customers
{
    public interface ICustomerRespository
    {
        Task AddAsync(Customer customer);
    }
}
