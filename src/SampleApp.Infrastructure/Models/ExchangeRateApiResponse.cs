using Newtonsoft.Json;

namespace SampleApp.Infrastructure.Models
{
    public class ExchangeRateApiResponse
    {
        public string Result { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
}
