using CA.And.DDD.Template.Application.Shared;
using MassTransit;

namespace CA.And.DDD.Template.Application.Authentication.RefreshUserToken
{
    public sealed class RefreshUserTokenCommandHandler : IConsumer<RefreshUserTokenCommand>
    {
        private readonly IAuthenticationService _keycloakAuthService;

        public RefreshUserTokenCommandHandler(IAuthenticationService keycloakAuthService)
        {
            _keycloakAuthService = keycloakAuthService;
        }

        public async Task Consume(ConsumeContext<RefreshUserTokenCommand> query)
        {
            (string accessToken, string refreshToken) tokens = await _keycloakAuthService
                .RefreshAccessTokenAsync(query.Message.RefreshToken);

            await query.RespondAsync(new RefreshUserTokenDto(tokens.accessToken, tokens.refreshToken));
        }
    }
}
