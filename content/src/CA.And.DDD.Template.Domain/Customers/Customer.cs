using CA.And.DDD.Template.Domain.Customers.DomainEvents;

namespace CA.And.DDD.Template.Domain.Customers
{
    public class Customer : Entity
    {
        public CustomerId CustomerId { get; private set; }
        public FullName FullName { get; private set; }
        public Age Age { get; private set; }
        public Email Email { get; private set; }
        public bool IsEmailVerified {  get; private set; }
        public Address Address {  get; private set; }

        private Customer() {}

        private Customer(CustomerId customerId, FullName fullName, Age age, Email email, Address address)
        {
            CustomerId = customerId;
            FullName = fullName;
            Age = age;
            Email = email;
            IsEmailVerified = false;
            AddDomainEvent(new CustomerCreatedDomainEvent(this.CustomerId.Value));
            Address = address;
        }

        public static Customer CreateCustomer(CustomerId customerId, FullName fullName, Age age, Email email, Address address)
        {
            return new Customer(customerId, fullName, age, email, address);
        }

        public void ChangeEmail(Email email) 
        {
            if(Email != email)
            {
                Email = email;
                AddDomainEvent(new CustomerEmailChnagedDomainEvent(email.Value, Email.Value));
            }
        }

        public void VerifyEmailAddress()
        {
            if(IsEmailVerified == false)
            {
                IsEmailVerified = true;
            }

            AddDomainEvent(new CustomerEmailVerifiedDomainEvent(Email.Value));
        }

    }
}
