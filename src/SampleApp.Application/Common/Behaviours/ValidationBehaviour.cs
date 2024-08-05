using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SampleApp.Application.Common.Exceptions;

namespace SampleApp.Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators.Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null);
            var validationFailures = failures as ValidationFailure[] ?? failures.ToArray();
            if (validationFailures.Any())
            {
                throw new ValidationFailedException(validationFailures);
            }

            return next();
        }
    }
}
