using Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer;
using Clean.Architecture.And.DDD.Template.Domian.Customers;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Customers;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;


namespace Application.UnitTests.Customer.CreateCustomerTests
{
    public class CreateCustomerTests
    {

        [Fact]
        public async Task Should_Create_Customer()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddConsumer<CreateCustomerCommandHandler>();
                
            })
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped(_  => mockCustomerRepository.Object)
            .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDatabase"))
            .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            await harness.Start();
            var email = "me@somewhere.com";
            var command = new CreateCustomerCommand("Mikoalj Jankowski", DateTime.Now.AddYears(-30), email, "Fifth Avenue", "10A", "1", "PL", "01-864");


            var client = harness.GetRequestClient<CreateCustomerCommand>();
            var response = client.GetResponse<CreateCustomerResponse>(command);

            Assert.Equal(email, response.Result.Message.Email);
            Assert.True(await harness.Sent.Any<CreateCustomerResponse>());
            Assert.True(await harness.Consumed.Any<CreateCustomerCommand>());
            mockCustomerRepository.Verify(repo => repo.AddAsync(It.IsAny<Clean.Architecture.And.DDD.Template.Domian.Customers.Customer>(),default), Times.Exactly(1));

        }
    }
}
