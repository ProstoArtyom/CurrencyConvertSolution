using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Enums;

namespace CurrencyConvert.Infrastructure.Providers
{
    public class BankRateProviderFactory : IBankRateProviderFactory
    {
        private readonly IEnumerable<IBankRateProvider> _providers;
        public BankRateProviderFactory(IEnumerable<IBankRateProvider> providers)
        {
            _providers = providers;
        }

        public IBankRateProvider GetProvider(BankCode bankCode)
        {
            return _providers.FirstOrDefault(p => p.BankCode == bankCode)
                   ?? throw new NotSupportedException($"Bank {bankCode} is not supported");
        }
    }
}
