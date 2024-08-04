namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer
{
    public sealed record CreateCustomerCommand(string FullName, DateTime BirthDate, string Email);
}
