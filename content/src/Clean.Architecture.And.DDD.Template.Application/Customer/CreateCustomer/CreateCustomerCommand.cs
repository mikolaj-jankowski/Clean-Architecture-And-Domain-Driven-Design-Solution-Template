namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer
{
    public record CreateCustomerCommand(string Name, string Surname, DateTime BirthDate);
}
