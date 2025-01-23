using CA.And.DDD.Template.Application.Authentication.LoginUser;
using CA.And.DDD.Template.Application.Authentication.RefreshUserToken;
using CA.And.DDD.Template.Application.Authentication.ReLoginCustomer;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CA.And.DDD.Template.WebApi.Controllers
{
    /// <summary>
    /// API for managing user authentication and token refresh.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("User authentication and token management")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Logs in a user and returns an authentication token.
        /// </summary>
        /// <param name="command">Login request containing user credentials.</param>
        /// <returns>Returns the authentication token and user details.</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginUserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "User login",
            Description = "Authenticates the user based on provided credentials and returns a JWT token."
        )]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var client = _mediator.CreateRequestClient<LoginUserCommand>();
            var response = await client.GetResponse<LoginUserDto>(command);
            return Ok(response.Message);

        }

        /// <summary>
        /// Refreshes an expired authentication token.
        /// </summary>
        /// <param name="command">Request containing the refresh token.</param>
        /// <returns>Returns a new authentication token.</returns>
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshUserTokenDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "Refresh authentication token",
            Description = "Generates a new authentication token using a valid refresh token."
        )]
        public async Task<IActionResult> RefreshToken(RefreshUserTokenCommand command)
        {
            var client = _mediator.CreateRequestClient<RefreshUserTokenCommand>();
            var response = await client.GetResponse<RefreshUserTokenDto>(command);
            return Ok(response.Message);

        }

    }
}
