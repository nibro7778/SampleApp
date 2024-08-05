namespace SampleApp.Api.Models
{
    public class ExchangeRateRequest
    {
        public decimal Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
    }
}
