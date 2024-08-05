using MediatR;

namespace SampleApp.Application.UseCases.Covert
{
    public class Request : IRequest<Response>
    {
        public decimal Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
    }
}
