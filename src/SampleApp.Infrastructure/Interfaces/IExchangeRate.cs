using Refit;
using SampleApp.Infrastructure.Models;

namespace SampleApp.Infrastructure.Interfaces
{
    public interface IExchangeRate
    {
        [Get("/v6/latest/{baseCode}")]
        Task<ExchangeRateApiResponse> GetRate(string baseCode);
    }
}
