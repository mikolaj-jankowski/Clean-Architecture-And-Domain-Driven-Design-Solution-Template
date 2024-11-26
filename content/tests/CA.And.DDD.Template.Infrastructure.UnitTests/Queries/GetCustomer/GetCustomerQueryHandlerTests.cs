using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Application.Customer.Shared;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CA.And.DDD.Template.Infrastructure.UnitTests.Queries.GetCustomer
{
    public class GetCustomerQueryHandlerTests
    {
        private readonly Mock<ICacheService> _cacheServiceMock = new Mock<ICacheService>();
        private readonly Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>();
        private ServiceProvider _provider;
        private ITestHarness _harness;

        private Customer _customer = Customer.CreateCustomer(
                new CustomerId(Guid.NewGuid()),
                new FullName("Mikolaj Jankowski"),
                new Age(DateTime.UtcNow.AddYears(-20)),
                new Email("my-email@yahoo.com"),
                new Address("Fifth Avenue", "10A", "1", "USA", "10037"));

        private void SetupProviderAndHarness()
        {
            _provider = new ServiceCollection()
                .AddMassTransitTestHarness(x => x.AddConsumer<GetCustomerQueryHandler>())
                .AddSingleton(_cacheServiceMock.Object)
                .AddScoped<ICustomerRepository>(_ =>_customerRepository.Object)
                .AddDbContext<IAppDbContext, AppDbContext>(options => options.UseInMemoryDatabase("TestDatabase"))
                .BuildServiceProvider(true);

            _harness = _provider.GetRequiredService<ITestHarness>();
        }

        [Fact]
        public async Task Should_Get_Customer_From_Cache()
        {
            //Arrange

            SetupProviderAndHarness();

            var harness = _provider.GetRequiredService<ITestHarness>();

            var expectedCustomer = new CustomerDto(Guid.NewGuid(), "Mikolaj Jankowski", 35, "mikolaj.jankowski@somedomain.com");

            _cacheServiceMock
                .Setup(repo => repo.GetAsync<CustomerDto>(CA.And.DDD.Template.Application.Shared.CacheKeyBuilder.GetCustomerKey(expectedCustomer.Email), default))
                .ReturnsAsync(expectedCustomer);

            await harness.Start();
            var query = new GetCustomerQuery(expectedCustomer.Email);


            var client = harness.GetRequestClient<GetCustomerQuery>();

            //Act
            var response = await client.GetResponse<CustomerDto>(query);

            //Assert
            Assert.True(await harness.Sent.Any<CustomerDto>());
            _cacheServiceMock.Verify(repo => repo.GetAsync<CustomerDto>(It.IsAny<string>(), default), Times.Exactly(1));
            _customerRepository.Verify(repo => repo.GetAsync(It.IsAny<string>(), default), Times.Exactly(0));

            Assert.Equal(response.Message, expectedCustomer);

        }


        [Fact]
        public async Task Should_Get_Customer_From_Db_When_Not_Present_In_Cache()
        {
            //Arrange
            SetupProviderAndHarness();

            var harness = _provider.GetRequiredService<ITestHarness>();

            _cacheServiceMock
                .Setup(repo => repo.GetAsync<CustomerDto?>(CA.And.DDD.Template.Application.Shared.CacheKeyBuilder.GetCustomerKey(_customer.Email.Value), default))
                .ReturnsAsync((CustomerDto?)null);

            _customerRepository
                .Setup(repo => repo.GetAsync(_customer.Email.Value, default))
                .ReturnsAsync(_customer);

            await harness.Start();

            var client = harness.GetRequestClient<GetCustomerQuery>();

            //Act
            var response = await client.GetResponse<CustomerDto>(new GetCustomerQuery(_customer.Email.Value));

            //Assert
            Assert.True(await harness.Sent.Any<CustomerDto>());
            _cacheServiceMock.Verify(repo => repo.GetAsync<CustomerDto>(It.IsAny<string>(), default), Times.Exactly(1));
            _customerRepository.Verify(repo => repo.GetAsync(It.IsAny<string>(), default), Times.Exactly(1));
            Assert.Equal(response.Message, _customer.ToDto());

        }

    }
}
