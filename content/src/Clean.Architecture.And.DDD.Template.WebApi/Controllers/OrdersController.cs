using Clean.Architecture.And.DDD.Template.Application.Order.CreateOrder;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(ILogger<OrderController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateOrder")]
        public async Task<IActionResult> Post()
        {
            await _mediator.Send<CreateOrderCommand>(new CreateOrderCommand("wall street 29","0992-1", Guid.Parse("219ad96f-5386-4a33-9faa-a0b0f4ef68a6")));            
            return Ok();
        }
    }
}
