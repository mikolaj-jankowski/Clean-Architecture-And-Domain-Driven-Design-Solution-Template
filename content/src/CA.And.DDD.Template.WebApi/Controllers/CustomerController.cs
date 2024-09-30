using CA.And.DDD.Template.Application.Customer.ChangeEmail;
using CA.And.DDD.Template.Application.Customer.CreateCustomer;
using CA.And.DDD.Template.Application.Customer.GetCustomer;
using CA.And.DDD.Template.Application.Customer.VerifyEmail;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace CA.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            var client = _mediator.CreateRequestClient<CreateCustomerCommand>();
            var response = await client.GetResponse<CreateCustomerResponse>(command);
            return Ok(response);
        }

        [HttpPost("change-email")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailCommand command)
        {
            await _mediator.Send<ChangeEmailCommand>(command);
            return NoContent();
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailCommand command)
        {
            await _mediator.Send<VerifyEmailCommand>(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer([FromQuery] GetCustomerQuery query)
        {
            var client = _mediator.CreateRequestClient<GetCustomerQuery>();
            var response = await client.GetResponse<GetCustomerQueryResponse>(query);
            return Ok(response);
        }
    }
}
