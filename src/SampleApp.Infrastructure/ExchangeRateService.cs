using Ardalis.Result;
using SampleApp.Infrastructure.Interfaces;
using IExchangeRateService = SampleApp.Application.Interfaces.IExchangeRateService;

namespace SampleApp.Infrastructure
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRate _exchangeRate;

        public ExchangeRateService(IExchangeRate exchangeRate)
        {
            _exchangeRate = exchangeRate;
        }

        public async Task<Result<decimal>> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            var response = await _exchangeRate.GetRate(fromCurrency);

            if (response.Result == "success")
            {
                return (decimal) response.Rates.Single(x => x.Key == toCurrency).Value;
            }

            //TODO: can return specific error message from external service itself
            return Result.Error("Unable to receive rate from external service");
            
        }
    }
}
