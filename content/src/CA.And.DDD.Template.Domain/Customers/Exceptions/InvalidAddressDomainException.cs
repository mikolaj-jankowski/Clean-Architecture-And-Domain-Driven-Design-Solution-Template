namespace CA.And.DDD.Template.Domain.Customers.Exceptions
{
    public class InvalidAddressDomainException : DomainException
    {
        public InvalidAddressDomainException(string fieldName) 
            : base($"The field of name '{fieldName}' is not valid.")
        {
        }
    }
}
