namespace CA.And.DDD.Template.Application.Customer.Shared
{
    public sealed record CustomerDto(Guid CustomerId, string FullName, int Age, string Email);

    public static class CustomerMapper
    {
        public static CustomerDto ToDto(this CA.And.DDD.Template.Domain.Customers.Customer customer)
        {
            return new CustomerDto(customer.CustomerId.Value, customer.FullName.Value, customer.Age.Value, customer.Email.Value);
        }
    }
}
