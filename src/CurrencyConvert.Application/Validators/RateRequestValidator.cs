using CurrencyConvert.Application.DTOs;
using CurrencyConvert.Domain.Enums;
using FluentValidation;

namespace CurrencyConvert.Application.Validators
{
    public class RateRequestValidator : AbstractValidator<RateRequestDto>
    {
        public RateRequestValidator()
        {
            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency code must be 3 characters.");

            RuleFor(x => x.Bank)
                .NotEmpty().WithMessage("Bank is required.")
                .Must(bank => Enum.TryParse<BankCode>(bank, ignoreCase: true, out _))
                .WithMessage($"Unknown bank. Available: {string.Join(", ", Enum.GetNames<BankCode>())}");
        }
    }
}
