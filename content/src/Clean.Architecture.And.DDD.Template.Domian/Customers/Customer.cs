using Clean.Architecture.And.DDD.Template.Domian.Customers.DomainEvents;

namespace Clean.Architecture.And.DDD.Template.Domian.Customers
{
    public class Customer : Entity
    {
        public CustomerId CustomerId { get; private set; }
        public FullName FullName { get; private set; }
        public Age Age { get; private set; }
        public Email Email { get; private set; }
        public bool IsEmailVerified {  get; private set; }

        private Customer() {}

        private Customer(CustomerId customerId, FullName fullName, Age age, Email email)
        {
            CustomerId = customerId;
            FullName = fullName;
            Age = age;
            Email = email;
            IsEmailVerified = false;
            AddDomainEvent(new CustomerCreatedDomainEvent(this.CustomerId.Value));
        }

        public static Customer CreateCustomer(CustomerId customerId, FullName fullName, Age age, Email email)
        {
            return new Customer(customerId, fullName, age, email);
        }

        public void ChangeEmail(Email email) 
        {
            if(Email != email)
            {
                Email = email;
                AddDomainEvent(new CustomerEmailChnagedDomainEvent(email, Email));
            }
        }

        public void VerifyEmailAddress()
        {
            if(IsEmailVerified == false)
            {
                IsEmailVerified = true;
            }

            AddDomainEvent(new CustomerEmailVerifiedDomainEvent(Email));
        }

    }
}
