using Clean.Architecture.And.DDD.Template.Application.Order.CreateOrder;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost(Name = "CreateOrder")]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {
            await _mediator.Send<CreateOrderCommand>(command);
            return NoContent();
        }
    }
}
