using MediatR;
using SampleApp.Application.Common.Exceptions;
using SampleApp.Application.Interfaces;

namespace SampleApp.Application.UseCases.Covert
{
    public class Handler(IExchangeRateService exchangeRateService) : IRequestHandler<Request, Response>
    {
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await exchangeRateService.GetExchangeRateAsync(request.InputCurrency, request.OutputCurrency);

            if (result.IsSuccess)
            {
                var value = request.Amount * result.Value;
                return new Response()
                {
                    Value = value
                };
            }

            var errors = result.Errors.Select(x => x);
            throw new ExternalServiceException(errors);
        }
    }
}
