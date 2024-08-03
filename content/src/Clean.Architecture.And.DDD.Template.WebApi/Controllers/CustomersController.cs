using Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(ILogger<CustomerController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(Name = "CreateCustomer")]
        public async Task<IActionResult> Post()
        {
            //await _mediator.Send<CreateCustomerCommand>(new CreateCustomerCommand("wall street 29", "0992-1", DateTime.Now.AddYears(-10)));
            var client = _mediator.CreateRequestClient<CreateCustomerCommand>();
            var response = await client.GetResponse<CreateCustomerResponse>(new CreateCustomerCommand("wall street 29", "0992-1", DateTime.Now.AddYears(-10)));
            return Ok(response);
        }
    }
}
