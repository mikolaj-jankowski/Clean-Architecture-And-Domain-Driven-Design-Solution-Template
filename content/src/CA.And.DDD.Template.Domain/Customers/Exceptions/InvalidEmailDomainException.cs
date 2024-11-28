namespace CA.And.DDD.Template.Domain.Customers.Exceptions
{
    public class InvalidEmailDomainException : DomainException
    {
        public InvalidEmailDomainException(string email)
            : base($"The email address '{email}' is not valid.")
        {
        }
    }
}
