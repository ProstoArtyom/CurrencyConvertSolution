using CurrencyConvert.Application.DTOs;
using CurrencyConvert.Domain.Enums;
using FluentValidation;

namespace CurrencyConvert.Application.Validators
{
    public class ConvertRequestValidator : AbstractValidator<ConvertRequestDto>
    {
        public ConvertRequestValidator()
        {
            RuleFor(x => x.From)
                .NotEmpty().WithMessage("From currency is required.")
                .Length(3).WithMessage("Currency code must be 3 characters.");

            RuleFor(x => x.To)
                .NotEmpty().WithMessage("To currency is required.")
                .Length(3).WithMessage("Currency code must be 3 characters.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.Bank)
                .NotEmpty().WithMessage("Bank is required.")
                .Must(bank => Enum.TryParse<BankCode>(bank, ignoreCase: true, out _))
                .WithMessage($"Unknown bank. Available: {string.Join(", ", Enum.GetNames<BankCode>())}");
        }
    }
}
