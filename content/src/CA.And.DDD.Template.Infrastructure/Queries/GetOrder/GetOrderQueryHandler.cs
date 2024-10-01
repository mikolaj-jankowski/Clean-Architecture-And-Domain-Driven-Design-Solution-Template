using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Application.Exceptions;
using CA.And.DDD.Template.Application.Order;
using CA.And.DDD.Template.Domain.Orders;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CA.And.DDD.Template.Infrastructure.Queries.GetCustomer
{
    public sealed class GetOrderQueryHandler : IConsumer<GetOrderQuery>
    {
        private readonly AppDbContext _appDbContext;

        public GetOrderQueryHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Consume(ConsumeContext<GetOrderQuery> query)
        {
            var id = query.Message.Id;
            var order = await _appDbContext
                .Set<Order>()
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OrderItems)
                .Where(x => ((Guid)x.OrderId) == id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new OrderNotFoundApplicationException(id);
            }
            await query.RespondAsync(
                new GetOrderQueryResponse(
                    order.OrderId.Value,
                    order.OrderItems.MapToOrderItemDto()
                    )
                );
        }
    }
}
