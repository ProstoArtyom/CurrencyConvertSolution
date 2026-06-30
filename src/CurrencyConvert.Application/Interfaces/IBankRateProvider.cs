using CurrencyConvert.Domain.Enums;

namespace CurrencyConvert.Application.Interfaces
{
    public interface IBankRateProvider
    {
        BankCode BankCode { get; }
        Task<decimal> GetRateAsync(string currencyCode, RateDirection direction, CancellationToken ct);
        Task<bool> SupportsCurrencyAsync(string currencyCode, CancellationToken ct);
    }
}
