using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Enums;

namespace CurrencyConvert.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IBankRateProviderFactory _factory;
        public CurrencyService(IBankRateProviderFactory factory)
        {
            _factory = factory;
        }

        public async Task<decimal> ConvertAsync(
            string fromCurrency, string toCurrency,
            decimal amount, BankCode bankCode,
            CancellationToken ct)
        {
            var provider = _factory.GetProvider(bankCode);
            var rate = await provider.GetRateAsync(fromCurrency, ct);
            return amount * rate;
        }
    }
}
