using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Domain.Customers.DomainEvents;
using CA.And.DDD.Template.Domain.Customers.Exceptions;

namespace CA.And.DDD.Template.Domain.UnitTests
{
    public class CustomerTests
    {
        [Theory]
        [InlineData("incomplete-email@")]
        [InlineData("sample.email")]
        internal void Should_Throw_Invalid_Email_Domain_Exception_For_Invalid_Email(string invalidEmail)
        {
            Assert.Throws<InvalidEmailDomainException>(() =>
            {
                new Email(invalidEmail);
            });
        }

        [Theory]
        [InlineData("AB")]
        [InlineData("Jonathan Alexander Montgomery-Harrington Fitzwilliam Cunningham-Smithington Kensington Wetherby-Clarkson Paddington-Livingston O'Sullivan Carrington-Rutherford")]
        internal void Should_Throw_Invalid_FullName_Domain_Exception_For_Too_Short_Or_Too_Long_FullName(string fullName)
        {
            Assert.Throws<InvalidFullNameDomainException>(() =>
            {
                new FullName(fullName);
            });
        }

        [Fact]
        internal void Should_Throw_Invalid_Age_Domain_Exception_When_Age_Is_Under_18_Years_Old()
        {
            Assert.Throws<InvalidCustomerAgeDomainException>(() =>
            {
                new Age(DateTime.UtcNow.AddYears(-16));
            });
        }


        [Fact]
        internal void Should_Create_Customer_For_Valid_Input_Data()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var fullName = new FullName("Mikolaj Jankowski");
            var age = new Age(DateTime.UtcNow.AddYears(-20));
            var email = new Email("my-email@yahoo.com");
            var address = new Address("Fifth Avenue", "10A", "1", "USA", "10037");

            // Act
            var customer = Customer.CreateCustomer(
                customerId,
                fullName,
                age,
                email,
                address);

            // Assert
            var domainEvents = customer.DomainEvents;
            Assert.NotNull(domainEvents);
            Assert.Single(domainEvents);

            var domainEvent = domainEvents.FirstOrDefault();
            Assert.NotNull(domainEvent);
            Assert.IsType<CustomerCreatedDomainEvent>(domainEvent);
        }

        [Fact]
        internal void Should_Change_Customer_Email()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var fullName = new FullName("Mikolaj Jankowski");
            var age = new Age(DateTime.UtcNow.AddYears(-20));
            var email = new Email("my-email@yahoo.com");
            var address = new Address("Fifth Avenue", "10A", "1", "USA", "10037");

            // Act
            var customer = Customer.CreateCustomer(
                customerId,
                fullName,
                age,
                email,
                address);

            customer.ChangeEmail(new Email("new-email-address@yahoo.com"));

            // Assert
            var domainEvents = customer.DomainEvents;
            Assert.NotNull(domainEvents);
            Assert.Equal(2, domainEvents.Count);

            Assert.Single(domainEvents.OfType<CustomerCreatedDomainEvent>());
            Assert.Single(domainEvents.OfType<CustomerEmailChangedDomainEvent>());
        }

        [Fact]
        internal void Should_Verify_Customer_Email()
        {
            // Arrange
            var customerId = new CustomerId(Guid.NewGuid());
            var fullName = new FullName("Mikolaj Jankowski");
            var age = new Age(DateTime.UtcNow.AddYears(-20));
            var email = new Email("my-email@yahoo.com");
            var address = new Address("Fifth Avenue", "10A", "1", "USA", "10037");

            // Act
            var customer = Customer.CreateCustomer(
                customerId,
                fullName,
                age,
                email,
                address);

            customer.VerifyEmailAddress();

            // Assert
            var domainEvents = customer.DomainEvents;
            Assert.NotNull(domainEvents);
            Assert.Equal(2, domainEvents.Count);

            Assert.Single(domainEvents.OfType<CustomerCreatedDomainEvent>());
            Assert.Single(domainEvents.OfType<CustomerEmailVerifiedDomainEvent>());
        }
    }
}