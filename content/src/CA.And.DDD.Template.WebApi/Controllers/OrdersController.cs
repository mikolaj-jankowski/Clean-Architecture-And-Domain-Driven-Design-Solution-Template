using CA.And.DDD.Template.Application.Order.BrowseOrders;
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


        [HttpGet("{orderId}")]
        [SwaggerOperation(Summary = "Retrieves an order")]
        [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrder([FromRoute] GetOrderQuery query)
        {
            var client = _mediator.CreateRequestClient<GetOrderQuery>();
            var response = await client.GetResponse<OrderDto>(query);
            return Ok(response.Message);
        }

        [HttpPost("browse-orders")]
        [SwaggerOperation(Summary = "Browse orders")]
        [ProducesResponseType(typeof(BrowseOrdersDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> BrowseOrders([FromBody] BrowseOrdersQuery query)
        {
            var client = _mediator.CreateRequestClient<BrowseOrdersQuery>();
            var response = await client.GetResponse<BrowseOrdersDto>(query);
            return Ok(response.Message);
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Creates a new order")]
        [ProducesResponseType(typeof(CreateOrderCommandResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var client = _mediator.CreateRequestClient<CreateOrderCommand>();
            var response = await client.GetResponse<CreateOrderCommandResponse>(command);
            return CreatedAtAction(nameof(GetOrder), new { orderId = response.Message.OrderId }, response.Message);
        }
    }
}
