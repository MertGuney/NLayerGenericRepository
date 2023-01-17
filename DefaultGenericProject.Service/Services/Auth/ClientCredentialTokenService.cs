using DefaultGenericProject.Core.Configuration;
using DefaultGenericProject.Core.Services;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace DefaultGenericProject.Service.Services.Auth
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly Client _clientSettings;

        public ClientCredentialTokenService(HttpClient httpClient, IOptions<Client> clientSettings)
        {
            _httpClient = httpClient;
            _clientSettings = clientSettings.Value;
        }

        public async Task<string> GetToken()
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "identityUrl",
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });
            if (disco.IsError)
            {
                throw disco.Exception;
            }
            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.Id,
                ClientSecret = _clientSettings.Secret,
                Address = disco.TokenEndpoint
            };
            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            if (newToken.IsError)
            {
                throw newToken.Exception;
            }
            return newToken.AccessToken;
        }
    }
}