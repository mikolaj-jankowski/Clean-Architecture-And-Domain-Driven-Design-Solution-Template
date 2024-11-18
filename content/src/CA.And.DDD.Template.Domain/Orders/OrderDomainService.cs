namespace CA.And.DDD.Template.Domain.Orders
{
    public class OrderDomainService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderDomainService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Money> CalculateDiscountAsync(Order order)
        {
            var totalSpentMoneyInLast31Days = await _orderRepository.GetTotalSpentInLast31DaysAsync(order.CustomerId);
            var discount = order.ApplyDiscount(new Money(totalSpentMoneyInLast31Days));
            var orderAmount = order.OrderItems.Sum(x => x.Quantity * x.Price.Amount);
            var discountAmount = orderAmount * discount;
            var totalAmount = orderAmount - discountAmount;
            return new Money(totalAmount);
        }
    }
}
