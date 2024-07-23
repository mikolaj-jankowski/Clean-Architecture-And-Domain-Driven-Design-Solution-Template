using Clean.Architecture.And.DDD.Template.Domian.Employees.DomainEvents;

namespace Clean.Architecture.And.DDD.Template.Domian.Employees
{
    public class Employee : Entity
    {
        public Guid EmployeeId { get; set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }

        private Employee()
        {

        }

        private Employee(string name, string surname)
        {
            Name = ValidateName(name);
            Surname = surname;
        }

        public static Employee CreateEmployee(string name, string surname)
        {
            return new Employee(name, surname);
        }

        private string ValidateName(string name)
        {
            AddDomainEvent(new EmployeeCreatedDomainEvent());

            if (!string.IsNullOrEmpty(name))
            {
                //throw new DomainException;
            }

            if (name.Length < 2)
            {
                //throw new DomainException;
            }
            if (name.Any(char.IsDigit))
            {
                //throw new DomainException;
            }
            return name;
            //validate characters count against db schema, 50 for name, 150 for surname
        }
    }
}
