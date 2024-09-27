namespace CA.And.DDD.Template.Application.Order.CreateOrder
{
    public sealed record CreateOrderCommand(string Street, string PostalCode, Guid CustomerId, ICollection<Product> Products);
    public sealed record Product(long ProductId, string ProductName, decimal Price, string Currency, uint Quantity);
}
