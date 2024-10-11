namespace CA.And.DDD.Template.Application.Customer.Shared
{
    public sealed record CustomerDto(Guid CustomerId, string FullName, int Age, string Email);
}
