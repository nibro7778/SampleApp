using Ardalis.Result;
using Moq;
using SampleApp.Application.Common.Exceptions;
using SampleApp.Application.Interfaces;
using SampleApp.Application.UseCases.Covert;

namespace SampleApp.Application.Tests.UseCases
{
    public class HandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsCorrectValue_WhenExchangeRateServiceSucceeds()
        {
            // Arrange
            var mockExchangeRateService = new Mock<IExchangeRateService>();
            var request = new Request
            {
                InputCurrency = "USD",
                OutputCurrency = "EUR",
                Amount = 100
            };
            var exchangeRate = 0.85; // Example exchange rate
            
            mockExchangeRateService
                .Setup(service => service.GetExchangeRateAsync(request.InputCurrency, request.OutputCurrency))
                .ReturnsAsync(() => new Result<decimal>((decimal)exchangeRate));

            var handler = new Handler(mockExchangeRateService.Object);
            var cancellationToken = new CancellationToken();

            // Act
            var response = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.Amount * (decimal)exchangeRate, response.Value);
        }

        [Fact]
        public async Task Handle_ThrowsExternalServiceException_WhenExchangeRateServiceFails()
        {
            // Arrange
            var mockExchangeRateService = new Mock<IExchangeRateService>();
            var request = new Request
            {
                InputCurrency = "USD",
                OutputCurrency = "EUR",
                Amount = 100
            };
            

            mockExchangeRateService
                .Setup(service => service.GetExchangeRateAsync(request.InputCurrency, request.OutputCurrency))
                .ReturnsAsync(() => Result.Error("Error fetching exchange rate"));

            var handler = new Handler(mockExchangeRateService.Object);
            var cancellationToken = new CancellationToken();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ExternalServiceException>(() => handler.Handle(request, cancellationToken));
            Assert.Contains("Error fetching exchange rate", exception.Error);
        }
    }
}