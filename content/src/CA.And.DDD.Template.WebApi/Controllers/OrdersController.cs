using CA.And.DDD.Template.Application.Order.CreateOrder;
using CA.And.DDD.Template.Application.Order.GetOrder;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CA.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Handles all operations related to orders, including creation, retrieval, updating, and deletion.")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves an order")]
        [ProducesResponseType(typeof(GetOrderQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder([FromQuery] GetOrderQuery query)
        {
            var client = _mediator.CreateRequestClient<GetOrderQuery>();
            var response = await client.GetResponse<GetOrderQueryResponse>(query);
            return Ok(response);
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Creates a new order")]
        [ProducesResponseType(typeof(CreateOrderResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var client = _mediator.CreateRequestClient<CreateOrderCommand>();
            var order = await client.GetResponse<CreateOrderResponse>(command);
            return CreatedAtAction(nameof(GetOrder), new GetOrderQuery(order.Message.OrderId), order);
        }
    }
}
