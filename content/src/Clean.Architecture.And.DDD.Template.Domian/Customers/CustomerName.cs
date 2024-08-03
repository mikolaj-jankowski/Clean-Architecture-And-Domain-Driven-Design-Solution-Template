using Clean.Architecture.And.DDD.Template.Domian.Customers.Exceptions;

namespace Clean.Architecture.And.DDD.Template.Domian.Customers
{
    public record CustomerName
    {
        public string Name { get; private set; }
        public CustomerName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new NameNotProvidedDomainException();
            }

            if(name.Length < 2)
            {
                throw new TooShortNameDomainException(name);
            }
            Name = name;
        }
    }
}
