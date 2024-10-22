using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Application.Customer.Shared;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using CA.And.DDD.Template.Infrastructure.Queries.GetCustomer;
using MassTransit;
using MassTransit.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CA.And.DDD.Template.Infrastructure.UnitTests.Queries.GetCustomer
{
    public class GetCustomerQueryHandlerTests
    {
        [Fact]
        public async Task Should_Get_Customer_From_Cache()
        {
            //Arrange
            var cacheServiceMock = new Mock<ICacheService>();
            var sqlQueries = new List<string>();

            await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddConsumer<GetCustomerQueryHandler>();

            })
            .AddSingleton<ICacheService>(cacheServiceMock.Object)
            .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDatabase").LogTo(sql => sqlQueries.Add(sql)))
            .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            var expectedCustomer = new CustomerDto(Guid.NewGuid(), "Mikolaj Jankowski", 35, "mikolaj.jankowski@somedomain.com");

            cacheServiceMock
                .Setup(repo => repo.GetAsync<CustomerDto>(CA.And.DDD.Template.Application.Shared.CacheKeyBuilder.GetCustomerKey(expectedCustomer.Email)))
                .ReturnsAsync(expectedCustomer);

            await harness.Start();
            var query = new GetCustomerQuery(expectedCustomer.Email);


            var client = harness.GetRequestClient<GetCustomerQuery>();

            //Act
            var response = await client.GetResponse<CustomerDto>(query);

            //Assert
            Assert.True(await harness.Sent.Any<CustomerDto>());
            cacheServiceMock.Verify(repo => repo.GetAsync<CustomerDto>(It.IsAny<string>()), Times.Exactly(1));
            Assert.Empty(sqlQueries);
            Assert.Equal(response.Message, expectedCustomer);

        }
    }
}
