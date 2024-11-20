using CA.And.DDD.Template.Application.Order.GetOrder;
using CA.And.DDD.Template.Application.Order.Shared;
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
            var orderReporistoryMock = new Mock<IOrderRepository>();

            await using var provider = new ServiceCollection()
            .AddMassTransitTestHarness(x =>
            {
                x.AddConsumer<GetOrderQueryHandler>();

            })
            .AddScoped<IOrderRepository>(_ => orderReporistoryMock.Object) 
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
                .Setup(repo => repo.GetAsync<OrderDto>(CA.And.DDD.Template.Application.Shared.CacheKeyBuilder.GetOrderKey(order.OrderId)))
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
            cacheServiceMock.Verify(repo => repo.GetAsync<OrderDto>(It.IsAny<string>()), Times.Exactly(1));
            orderReporistoryMock.Verify(repo => repo.GetOrderById(new OrderId(order.OrderId), default), Times.Exactly(0));

        }
    }
}
