using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SampleApp.Application.Common.Resolvers;

namespace SampleApp.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { ContractResolver = new SensitiveDataResolver() };

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestType = typeof(TRequest).FullName;
            var responseType = typeof(TResponse).FullName;

            _logger.LogInformation("Request - Handling request {requestType}->{responseType} {mediatorRequestBody}",
                requestType,
                responseType,
                SerializeObject(request));

            var response = await next();

            _logger.LogInformation(
                "Returning response {requestType}->{responseType} {mediatorResponseBody}",
                requestType,
                responseType,
                SerializeObject(response));

            return response;
        }
        private string SerializeObject(object obj) => JsonConvert.SerializeObject(obj, _jsonSettings);
    }
}
