namespace SampleApp.Api.Models
{
    public class ExchangeRateResponse
    {
        public decimal Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
        public decimal Value { get; set; }
    }
}
