using CA.And.DDD.Template.Application.Customer.ChangeEmail;
using CA.And.DDD.Template.Application.Customer.CreateCustomer;
using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Application.Customer.VerifyEmail;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CA.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [HttpGet]
        [ProducesResponseType(typeof(GetCustomerQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer([FromQuery] GetCustomerQuery query)
        {
            var client = _mediator.CreateRequestClient<GetCustomerQuery>();
            var response = await client.GetResponse<GetCustomerQueryResponse>(query);
            return Ok(response);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(CreateCustomerResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            var client = _mediator.CreateRequestClient<CreateCustomerCommand>();
            var customer = await client.GetResponse<CreateCustomerResponse>(command);
            return CreatedAtAction(nameof(GetCustomer), new GetCustomerQuery(customer.Message.Email), customer);
        }

        [HttpPost("change-email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangeEmail(ChangeEmailCommand command)
        {
            await _mediator.Send<ChangeEmailCommand>(command);
            return NoContent();
        }

        [HttpPost("verify-email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> VerifyEmail(VerifyEmailCommand command)
        {
            await _mediator.Send<VerifyEmailCommand>(command);
            return NoContent();
        }

    }
}
