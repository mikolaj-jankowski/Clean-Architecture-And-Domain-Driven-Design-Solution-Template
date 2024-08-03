using Clean.Architecture.And.DDD.Template.Domian.Customers.Exceptions;

namespace Clean.Architecture.And.DDD.Template.Domian.Customers
{
    public record CustomerSurname
    {
        public string Surname { get; private set; }
        public CustomerSurname(string surname)
        {
            if (string.IsNullOrEmpty(surname))
            {
                throw new SurnameNotProvidedDomainException();
            }
            Surname = surname;
        }
    }
}
