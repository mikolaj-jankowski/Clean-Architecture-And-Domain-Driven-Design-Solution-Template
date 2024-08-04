namespace Clean.Architecture.And.DDD.Template.Application.Exceptions
{
    public class CustomerNotFoundApplicationException : ApplicationException
    {
        public CustomerNotFoundApplicationException(string email) 
            : base($"Customer with email '{email}' has not been found")
        {
        }
    }
}
