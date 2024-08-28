using Clean.Architecture.And.DDD.Template.Application.Shared;

namespace Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer
{
    public sealed record CustomerCreatedIntegrationEvent(Guid CustomerId);
}