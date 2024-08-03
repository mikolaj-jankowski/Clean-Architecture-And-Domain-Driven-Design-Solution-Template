namespace Clean.Architecture.And.DDD.Template.Domian.Customers.Exceptions
{
    public class TooShortNameDomainException : DomainException
    {
        public TooShortNameDomainException(string name)
            : base($"Name of {name} is too short, at least 2 characters are required")
        {

        }
    }
}
