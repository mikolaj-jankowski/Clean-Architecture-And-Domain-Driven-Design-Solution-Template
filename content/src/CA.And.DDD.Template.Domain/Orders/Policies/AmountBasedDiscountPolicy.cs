namespace CA.And.DDD.Template.Domain.Orders.Policies
{
    public class AmountBasedDiscountPolicy
    {
        public decimal CalculateDiscount(Money totalSpentAmountInLast31Days, IReadOnlyCollection<OrderItem> orderItems)
        {
            var currentTotalAmount = orderItems.Sum(x => x.Quantity * x.Price.Amount);
            if (totalSpentAmountInLast31Days.Amount >= 500 || currentTotalAmount > 800)
            {
                return 0.05m;
            }
            else if (totalSpentAmountInLast31Days.Amount >= 250 || currentTotalAmount > 600)
            {
                return 0.025m;
            }
            else
            {
                return 0.00m;
            }
        }
    }
}
