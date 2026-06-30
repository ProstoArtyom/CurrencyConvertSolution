using System.Net.Http.Json;
using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace CurrencyConvert.Infrastructure.Providers.AlfaBank
{
    public class AlfaBankRateProvider : IBankRateProvider
    {
        public BankCode BankCode => BankCode.AlfaBank;

        private const string CacheKey = "alfabank_rates";
        private const string BaseCurrency = "BYN";

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        public AlfaBankRateProvider(
            IHttpClientFactory httpClientFactory,
            IMemoryCache cache)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(AlfaBankRateProvider));
            _cache = cache;
        }

        public async Task<decimal> GetRateAsync(string currencyCode, RateDirection direction, CancellationToken ct)
        {
            var rates = await GetRatesAsync(ct);
            var rate = FindRateToBase(rates, currencyCode)
                ?? throw new InvalidOperationException(
                    $"{BankCode.ToString()} returned no {BaseCurrency} rate for currency '{currencyCode}'.");

            var rawRate = direction == RateDirection.Sell
                ? rate.SellRate
                : rate.BuyRate;
            return rawRate / rate.Quantity;
        }

        public async Task<bool> SupportsCurrencyAsync(string currencyCode, CancellationToken ct)
        {
            var rates = await GetRatesAsync(ct);
            return FindRateToBase(rates, currencyCode) is not null;
        }

        private async Task<List<AlfaBankRateDto>> GetRatesAsync(CancellationToken ct)
        {
            if (_cache.TryGetValue(CacheKey, out List<AlfaBankRateDto>? cached))
                return cached!;

            var response = await _httpClient.GetFromJsonAsync<AlfaBankRateResponseDto>(
                $"public/rates", ct);
            var rates = response!.Rates;

            _cache.Set(CacheKey, rates, TimeSpan.FromMinutes(15));
            return rates!;
        }

        private static AlfaBankRateDto? FindRateToBase(List<AlfaBankRateDto> rates, string currencyCode)
        {
            return rates.FirstOrDefault(r =>
                r.SellIso.Equals(currencyCode, StringComparison.OrdinalIgnoreCase)
                && r.BuyIso == BaseCurrency);
        }
    }
}
