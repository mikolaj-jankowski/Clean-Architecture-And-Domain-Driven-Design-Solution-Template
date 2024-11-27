using CA.And.DDD.Template.Application.Order.GetOrder;
using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Domain.Customers;
using CA.And.DDD.Template.Domain.Orders;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CA.And.DDD.Template.Infrastructure.UnitTests.Queries.GetCustomer
{
    public class GetOrderQueryHandlerTests
    {
        [Fact]
        public async Task Should_Get_Order_From_Cache()
        {
            //Arrange
            var cacheServiceMock = new Mock<ICacheService>();
            var orderReadService = new Mock<IOrderReadService>();

            await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddConsumer<GetOrderQueryHandler>();

            })
            .AddScoped<IOrderReadService>(_ => orderReadService.Object) 
            .AddScoped<OrderDomainService>() 
            .AddSingleton<ICacheService>(cacheServiceMock.Object)
            .AddDbContext<IAppDbContext, AppDbContext>()
            .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            var customerId = new CustomerId(Guid.NewGuid());
            var shippingAddress = new ShippingAddress("Fifth Avenue 10A", "10037");
            var orderDate = DateTime.UtcNow;

            var order = Order.Create(customerId, shippingAddress, orderDate).ToDto();



            cacheServiceMock
                .Setup(repo => repo.GetAsync<OrderDto>(CA.And.DDD.Template.Application.Shared.CacheKeyBuilder.GetOrderKey(order.OrderId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(order);


            await harness.Start();
            var query = new GetOrderQuery(order.OrderId);


            var client = harness.GetRequestClient<GetOrderQuery>();

            //Act
            var response = await client.GetResponse<OrderDto>(query);

            //Assert
            Assert.True(await harness.Sent.Any<OrderDto>());
            Assert.Equal(response.Message.OrderId, order.OrderId);
            Assert.Equal(response.Message.OrderItems, order.OrderItems);
            cacheServiceMock.Verify(repo => repo.GetAsync<OrderDto>(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
            orderReadService.Verify(repo => repo.GetOrderById(order.OrderId, It.IsAny<CancellationToken>()), Times.Exactly(0));

        }
    }
}
