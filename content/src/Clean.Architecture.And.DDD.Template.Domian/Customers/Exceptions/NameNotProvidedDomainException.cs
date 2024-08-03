namespace Clean.Architecture.And.DDD.Template.Domian.Customers.Exceptions
{
    public class NameNotProvidedDomainException : DomainException
    {
        public NameNotProvidedDomainException()
            : base("Name of the customer is mandatory")
        {

        }
    }
}
