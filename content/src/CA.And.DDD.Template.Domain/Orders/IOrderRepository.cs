
namespace CA.And.DDD.Template.Domain.Orders
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<decimal> GetTotalSpentInLast31DaysAsync(Guid userId);
    }
}
