using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;

namespace Identity.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IHttpClientFactory _client;

        public ValuesController(IHttpClientFactory client)
        {
            _client = client;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var identityClient = _client.CreateClient();
            var disdoc = await identityClient.GetDiscoveryDocumentAsync("http://localhost:5000");

            var token = await identityClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = disdoc.TokenEndpoint,
                    ClientId = "client_id",
                    ClientSecret = "super_hard_to_guess",
                    Scope = "Api_1"
                }
            );

            var apiClient = _client.CreateClient();
            apiClient.SetBearerToken(token.AccessToken);
            var content = await apiClient.GetStringAsync("http://localhost:5020/WeatherForecast/get");
            return Ok(content);

        }
    }
}
