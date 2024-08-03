using Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents;
using Clean.Architecture.And.DDD.Template.Domian.Customers.Exceptions;

namespace Clean.Architecture.And.DDD.Template.Domian.Customers
{
    public class Customer : Entity
    {
        public CustomerId CustomerId { get; private set; }
        public CustomerName Name { get; private set; }
        public CustomerSurname Surname { get; private set; }
        public DateTime BirthDate { get; private set; }

        private Customer()
        {

        }

        private Customer(string name, string surname, DateTime birthDate, IDateTimeProvider dateTimeProvider)
        {
            CustomerId = new CustomerId(Guid.NewGuid());
            Name = new CustomerName(name);
            ValidateAge(birthDate, dateTimeProvider);
            Surname = new CustomerSurname(surname);
            AddDomainEvent(new CustomerCreatedDomainEvent(this.CustomerId.Value));
        }
        /// <summary>
        /// Checks weather a customer is at least 18 years old.
        /// </summary>
        /// <param name="birthDate"></param>
        /// <exception cref="CustomerAgeRequirementNotMetDomainException"></exception>
        private void ValidateAge(DateTime birthDate, IDateTimeProvider dateTimeProvider)
        {
            var age = dateTimeProvider.UtcNow.Year - birthDate.Year;
            if (birthDate.Date > dateTimeProvider.UtcNow.AddYears(-age)) age--;
            if(age < 18)
            {
                throw new CustomerAgeRequirementNotMetDomainException();
            }
            BirthDate = birthDate;
        }

        public static Customer CreateCustomer(string name, string surname, DateTime birthDate, IDateTimeProvider dateTimeProvider)
        {
            //check uniqueness
            return new Customer(name, surname, birthDate, dateTimeProvider);
        }

    }
}
