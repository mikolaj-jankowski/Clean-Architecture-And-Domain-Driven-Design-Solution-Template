namespace Clean.Architecture.And.DDD.Template.Domian.Customers.Exceptions
{
    public class CustomerAgeRequirementNotMetDomainException : DomainException
    {
        public CustomerAgeRequirementNotMetDomainException() 
            : base("Customer has to be at least 18 years old.")
        {
        }
    }
}
