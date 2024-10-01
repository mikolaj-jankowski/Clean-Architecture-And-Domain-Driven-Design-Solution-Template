using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Application.Order.CreateOrder;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CA.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(GetOrderQueryResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrder([FromQuery] GetOrderQuery query)
        {
            var client = _mediator.CreateRequestClient<GetOrderQuery>();
            var response = await client.GetResponse<GetOrderQueryResponse>(query);
            return Ok(response);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(CreateOrderResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var client = _mediator.CreateRequestClient<CreateOrderCommand>();
            var order = await client.GetResponse<CreateOrderResponse>(command);
            return CreatedAtAction(nameof(GetOrder), new GetOrderQuery(order.Message.OrderId), order);
        }
    }
}
