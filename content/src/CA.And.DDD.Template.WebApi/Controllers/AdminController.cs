using CA.And.DDD.Template.Application.Order.BrowseCustomers;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CA.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    [Route("api/[controller]")]
    [SwaggerTag("Admin operations (customer management)")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
            => _mediator = mediator;


        [HttpPost("browse-customers")]
        [SwaggerOperation(Summary = "Browse customers")]
        [ProducesResponseType(typeof(BrowseCustomersDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> BrowseCustomers([FromBody] BrowseCustomersQuery query)
        {
            var client = _mediator.CreateRequestClient<BrowseCustomersQuery>();
            var response = await client.GetResponse<BrowseCustomersDto>(query);
            return Ok(response.Message);
        }
    }
}
