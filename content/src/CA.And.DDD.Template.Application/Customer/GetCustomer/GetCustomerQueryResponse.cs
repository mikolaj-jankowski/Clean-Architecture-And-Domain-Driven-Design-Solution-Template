namespace CA.And.DDD.Template.Application.Customer.GetCustomer
{
    public sealed record GetCustomerQueryResponse(string FullName, int Age, string Email)
    {
    }
}
