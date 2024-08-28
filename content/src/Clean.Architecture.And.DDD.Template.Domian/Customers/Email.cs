using Clean.Architecture.And.DDD.Template.Domian.Customers;
using Clean.Architecture.And.DDD.Template.Domian.Customers.Exceptions;
using System.Text.RegularExpressions;

namespace Clean.Architecture.And.DDD.Template.Domian.Customers
{
    public sealed record Email
    {
        public string Value { get; init; }
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !EmailRegex.IsMatch(email))
            {
                throw new InvalidEmailDomainException(email);
            }

            Value = email;
        }
        public static explicit operator string(Email email)
            => email.Value;
    }
}
