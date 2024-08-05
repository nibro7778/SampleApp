using FluentValidation.Results;

namespace SampleApp.Application.Common.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public IEnumerable<ValidationFailure> Failures { get; }

        public ValidationFailedException(IEnumerable<ValidationFailure> failures)
        {
            Failures = failures;
        }
    }
}
