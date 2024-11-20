namespace CA.And.DDD.Template.Domain.Orders.Policies
{
    public class TotalSpentMoneyInLast31DaysDiscountPolicy
    {
        public decimal CalculateDiscount(Money totalSpentMoneyInLast31Days)
        {
            if (totalSpentMoneyInLast31Days.Amount > 4000)
            {
                return 0.70m;
            }
            else if (totalSpentMoneyInLast31Days.Amount > 2000)
            {
                return 0.45m;
            }
            else if (totalSpentMoneyInLast31Days.Amount > 1000)
            {
                return 0.25m;
            }
            else
            {
                return 0.00m;
            }
        }
    }
}
