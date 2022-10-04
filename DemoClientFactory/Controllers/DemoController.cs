using System.Text.Json;
using DemoClientFactory.Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoClientFactory.Controllers
{
    [ApiController]
    [Route("")]
    public class DemoController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DemoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("")]
        public async Task<IEnumerable<BurgerDto>?> OnGet()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,"/burgers");
            var httpClient = _httpClientFactory.CreateClient("BurgerApi");
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var burgers = await JsonSerializer.DeserializeAsync<IEnumerable<BurgerDto>>(
                    contentStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

                return burgers;
            }

            return new List<BurgerDto>();
        }
    }
}