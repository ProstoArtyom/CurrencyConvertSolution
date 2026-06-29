using CurrencyConvert.Domain.Enums;

namespace CurrencyConvert.Application.Interfaces
{
    public interface IBankRateProviderFactory
    {
        IBankRateProvider GetProvider(BankCode bankCode);
    }
}
