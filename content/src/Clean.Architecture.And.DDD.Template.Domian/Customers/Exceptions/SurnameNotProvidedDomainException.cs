namespace Clean.Architecture.And.DDD.Template.Domian.Customers.Exceptions
{
    public class SurnameNotProvidedDomainException : DomainException
    {
        public SurnameNotProvidedDomainException()
            : base("Surname of the customer is mandatory")
        {

        }
    }
}
