using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SampleApp.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {

        private readonly ILogger<PerformanceBehaviour<TRequest, TResponse>> _logger;

        public PerformanceBehaviour(ILogger<PerformanceBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var start = DateTime.UtcNow;
            var response = await next();
            var end = DateTime.UtcNow;
            var diff = end - start;
            var elapsedMilliseconds = (long)diff.TotalMilliseconds;

            var requestType = typeof(TRequest).FullName;

            if (elapsedMilliseconds > 1000)
            {
                _logger.LogWarning(
                    "Long Running Request: {requestType} total time {ElapsedMilliseconds} in milliseconds", requestType,
                    elapsedMilliseconds);

            }

            return response;
        }
    }
}
