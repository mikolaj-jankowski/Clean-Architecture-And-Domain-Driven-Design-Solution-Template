namespace Clean.Architecture.And.DDD.Template.Application.Order.CreateOrder
{
    public record CreateOrderCommand(string Street, string PostalCode, Guid CustomerId);
}
