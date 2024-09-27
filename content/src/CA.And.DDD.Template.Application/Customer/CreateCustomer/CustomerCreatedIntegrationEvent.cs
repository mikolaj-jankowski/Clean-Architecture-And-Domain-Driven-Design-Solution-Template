using CA.And.DDD.Template.Application.Shared;

namespace CA.And.DDD.Template.Application.Customer.CreateCustomer
{
    public sealed record CustomerCreatedIntegrationEvent(Guid CustomerId);
}