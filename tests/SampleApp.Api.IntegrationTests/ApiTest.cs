using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SampleApp.Api.Models;

namespace SampleApp.Api.IntegrationTests
{
    public class CurrencyExchangeControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CurrencyExchangeControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ExchangeRate_ReturnsOk_WhenRequestIsValid()
        {
            // Arrange
            var client = _factory.CreateClient();

            var request = new ExchangeRateRequest
            {
                InputCurrency = "USD",
                OutputCurrency = "EUR",
                Amount = 100
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/CurrencyExchange", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseBody = await response.Content.ReadAsStringAsync();
            var exchangeRateResponse = JsonConvert.DeserializeObject<ExchangeRateResponse>(responseBody);
            exchangeRateResponse.Should().NotBeNull();
            exchangeRateResponse.Value.Should().BeGreaterThan(0); // Ensure the value is greater than 0
        }

        
    }
}
