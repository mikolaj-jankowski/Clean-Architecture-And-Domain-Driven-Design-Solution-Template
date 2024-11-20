namespace CA.And.DDD.Template.Domain.Orders.Policies
{
    public class AmountBasedDiscountPolicy
    {
        public decimal CalculateDiscount(IReadOnlyCollection<OrderItem> orderItems)
        {
            var currentTotalAmount = orderItems.Sum(x => x.Quantity * x.Price.Amount);
            if (currentTotalAmount > 800)
            {
                return 0.05m;
            }
            else if (currentTotalAmount > 600)
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
