using CA.And.DDD.Template.Application.Shared;
using CA.And.DDD.Template.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CA.And.DDD.Template.Infrastructure.Authentication
{
    public class KeycloakAuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<AppSettings> _options;

        public KeycloakAuthenticationService(HttpClient httpClient, IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<(string accessToken, string refreshToken)> RequestTokenUsingPasswordGrantAsync(string username, string password)
        {
            var authOptions = _options.Value.Authentication;

            var values = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", authOptions.ClientId },
                { "username", username },
                { "password", password },
                { "scope", "offline_access e-commerce-scope" }
            };

            using var content = new FormUrlEncodedContent(values);

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(authOptions.TokenEndpoint, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to authenticate user.", ex);
            }

            var responseString = await response.Content.ReadAsStringAsync();

            var keycloakResponse = JsonSerializer.Deserialize<KeycloakTokenResponse>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (keycloakResponse is null || string.IsNullOrEmpty(keycloakResponse.AccessToken) || string.IsNullOrEmpty(keycloakResponse.RefreshToken))
            {
                throw new ApplicationException("Invalid authentication response received from the server.");
            }

            return (keycloakResponse.AccessToken, keycloakResponse.RefreshToken);
        }

        public async Task<(string accessToken, string refreshToken)> RefreshAccessTokenAsync(string refreshToken)
        {
            var authOptions = _options.Value.Authentication;

            var values = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", authOptions.ClientId },
                { "refresh_token", refreshToken }
            };

            using var content = new FormUrlEncodedContent(values);

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(authOptions.TokenEndpoint, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to refresh access token.", ex);
            }

            var responseString = await response.Content.ReadAsStringAsync();

            var keycloakResponse = JsonSerializer.Deserialize<KeycloakTokenResponse>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (keycloakResponse is null || string.IsNullOrEmpty(keycloakResponse.AccessToken) || string.IsNullOrEmpty(keycloakResponse.RefreshToken))
            {
                throw new ApplicationException("Invalid response received from OAuth2 server while refreshing the token.");
            }

            return (keycloakResponse.AccessToken, keycloakResponse.RefreshToken);
        }
    }

    public record KeycloakTokenResponse(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("refresh_token")] string RefreshToken
    );
}
