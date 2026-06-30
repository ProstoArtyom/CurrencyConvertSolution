using CurrencyConvert.Application.DTOs;
using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Enums;

namespace CurrencyConvert.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private const string BaseCurrency = "BYN";

        private readonly IBankRateProviderFactory _factory;
        public CurrencyService(IBankRateProviderFactory factory)
        {
            _factory = factory;
        }

        public async Task<ConvertResponseDto> ConvertAsync(
            string fromCurrency, string toCurrency,
            decimal amount, BankCode bankCode,
            CancellationToken ct)
        {
            var provider = _factory.GetProvider(bankCode);
            var rate = await GetCrossRateAsync(provider, fromCurrency, toCurrency, ct);
            return new ConvertResponseDto
            {
                From = fromCurrency,
                To = toCurrency,
                Amount = amount,
                Bank = bankCode.ToString(),
                Rate = rate,
                Result = Math.Round(amount * rate, 4),
            };
        }

        public async Task<RateResultDto> GetRateAsync(string currency, BankCode bankCode, CancellationToken ct)
        {
            if (currency.Equals(BaseCurrency, StringComparison.OrdinalIgnoreCase))
            {
                return new RateResultDto
                {
                    Currency = currency.ToUpper(),
                    BuyRate = 1m,
                    SellRate = 1m,
                    Bank = bankCode.ToString()
                };
            }

            var provider = _factory.GetProvider(bankCode);
            if (!await provider.SupportsCurrencyAsync(currency, ct))
                throw new NotSupportedException(
                    $"Currency '{currency}' is not supported by {bankCode}.");

            return new RateResultDto
            {
                Currency = currency.ToUpper(),
                BuyRate = await provider.GetRateAsync(currency, RateDirection.Buy, ct),
                SellRate = await provider.GetRateAsync(currency, RateDirection.Sell, ct),
                Bank = bankCode.ToString()
            };
        }

        private async Task<decimal> GetCrossRateAsync(
            IBankRateProvider provider,
            string from, string to,
            CancellationToken ct)
        {
            await ValidateCurrenciesAsync(provider, from, to, ct);

            if (to == BaseCurrency)
                return await provider.GetRateAsync(from, RateDirection.Sell, ct);

            if (from == BaseCurrency)
                return 1m / await provider.GetRateAsync(to, RateDirection.Buy, ct);

            var rateFrom = await provider.GetRateAsync(from, RateDirection.Sell, ct);
            var rateTo = await provider.GetRateAsync(to, RateDirection.Buy, ct);
            return rateFrom / rateTo;
        }

        private async Task ValidateCurrenciesAsync(
            IBankRateProvider provider,
            string from, string to,
            CancellationToken ct)
        {
            foreach (var currency in new[] { from, to }.Where(c => c != BaseCurrency))
            {
                if (!await provider.SupportsCurrencyAsync(currency, ct))
                    throw new NotSupportedException(
                        $"Currency '{currency}' is not supported by {provider.BankCode}.");
            }
        }
    }
}
