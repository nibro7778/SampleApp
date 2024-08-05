using FluentValidation;

namespace SampleApp.Application.UseCases.Covert
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Amount)
                .NotNull()
                .NotEmpty()
                .WithMessage("Amount must have value");

            //TODO: Can add validation for supported currency
            RuleFor(x => x.InputCurrency)
                .NotNull()
                .NotEmpty()
                .WithMessage("InputCurrency must have value");

            //TODO: Can add validation for supported currency
            RuleFor(x => x.OutputCurrency)
                .NotNull()
                .NotEmpty()
                .WithMessage("OutputCurrency must have value");

        }
    }
}
