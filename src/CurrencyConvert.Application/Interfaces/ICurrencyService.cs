using CurrencyConvert.Domain.Enums;

namespace CurrencyConvert.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<decimal> ConvertAsync(
            string fromCurrency, string toCurrency,
            decimal amount, BankCode bankCode,
            CancellationToken ct);
    }
}
