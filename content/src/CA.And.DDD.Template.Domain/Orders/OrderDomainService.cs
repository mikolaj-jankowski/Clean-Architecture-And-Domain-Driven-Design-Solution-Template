using CA.And.DDD.Template.Domain.Orders.Policies;

namespace CA.And.DDD.Template.Domain.Orders
{
    public class OrderDomainService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderDomainService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CalculateDiscountBaseOnLast31DaysSpendingAsync(Order order, CancellationToken cancellationToken)
        {
            var anyOtherDiscountWasNotApplied = order.Discount is null;
            if (order.OrderItems.Any() && anyOtherDiscountWasNotApplied)
            {
                var totalSpentMoneyInLast31Days = await _orderRepository.GetTotalSpentInLast31DaysAsync(order.CustomerId);

                if (totalSpentMoneyInLast31Days > 1000)
                {
                    var discount = new TotalSpentMoneyInLast31DaysDiscountPolicy().CalculateDiscount(new Money(totalSpentMoneyInLast31Days));
                    if (discount > 0)
                    {
                        var discountAmount = order.TotalAmount.Amount * discount;
                        order.Discount = new Discount(discountAmount, DiscountType.TotalSpentMoneyInLast31Days);
                        order.TotalAmount = new Money(order.TotalAmount.Amount - discountAmount);
                    }

                }
            }
        }
    }
}
