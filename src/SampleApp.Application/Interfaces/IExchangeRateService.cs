using Ardalis.Result;

namespace SampleApp.Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task<Result<decimal>> GetExchangeRateAsync(string fromCurrency, string toCurrency);
    }
}
