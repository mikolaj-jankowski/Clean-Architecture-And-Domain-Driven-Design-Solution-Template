using CA.And.DDD.Template.Domain.Customers.Exceptions;
using CA.And.DDD.Template.Domain.Orders;

namespace CA.And.DDD.Template.Domain.Customers
{
    public sealed record Age
    {
        public int Value { get; }
        public DateTime BirthDate { get; }
        public Age(DateTime dateOfBirth)
        {
            BirthDate = DateTime.SpecifyKind(dateOfBirth, DateTimeKind.Utc);
            Value = CalculateAge(dateOfBirth, DateTime.UtcNow);
        }

        private static int CalculateAge(DateTime birthDate, DateTime currentDate)
        {
            var age = currentDate.Year - birthDate.Year;
            if (birthDate.Date > currentDate.AddYears(-age)) age--;
            if (age < CustomerConstants.Customer.MinAllowedAge)
            {
                throw new InvalidCustomerAgeDomainException();
            }
            return age;
        }
    }
}
