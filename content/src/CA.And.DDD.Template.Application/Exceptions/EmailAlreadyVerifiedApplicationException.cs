namespace CA.And.DDD.Template.Application.Exceptions
{
    public class EmailAlreadyVerifiedApplicationException : ApplicationException
    {
        public EmailAlreadyVerifiedApplicationException(string email) 
            : base($"The email address '{email}' has already been verified.")
        {
        }
    }
}
