namespace CA.And.DDD.Template.Domian.Orders.Exceptions
{
    public class MaximumQuantityExceededDomainException : DomainException
    {
        public MaximumQuantityExceededDomainException()
            : base("Maximum allowed quantity has been exceeded")
        {
            
        }
    }
}
