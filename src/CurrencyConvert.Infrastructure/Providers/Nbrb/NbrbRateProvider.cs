using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace CurrencyConvert.Infrastructure.Providers.Nbrb
{
    public class NbrbRateProvider : IBankRateProvider
    {
        public BankCode BankCode => BankCode.Nbrb;

        private const string CacheKey = "nbrb_currencies";

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        public NbrbRateProvider(IHttpClientFactory httpClientFactory,
            IMemoryCache cache)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(NbrbRateProvider));
            _cache = cache;
        }

        public async Task<decimal> GetRateAsync(string currencyCode, RateDirection direction, CancellationToken ct)
        {
            var response = await _httpClient.GetFromJsonAsync<NbrbRateDto>(
                $"exrates/rates/{currencyCode}?parammode=2", ct)
                ?? throw new InvalidOperationException(
                    $"{BankCode.ToString()} returned no data for currency '{currencyCode}'."); ;

            return response!.Cur_OfficialRate / response.Cur_Scale;
        }

        public async Task<bool> SupportsCurrencyAsync(string currencyCode, CancellationToken ct)
        {
            var currencies = await GetSupportedCurrenciesAsync(ct);
            return currencies.Contains(currencyCode.ToUpper());
        }

        private async Task<HashSet<string>> GetSupportedCurrenciesAsync(CancellationToken ct)
        {
            if (_cache.TryGetValue(CacheKey, out HashSet<string>? cached))
                return cached!;

            var currencies = await _httpClient
                .GetFromJsonAsync<List<NbrbCurrencyDto>>(
                    "exrates/currencies", ct);

            var now = DateTime.UtcNow;

            var activeCodes = currencies!
                .Where(c => c.Cur_DateStart <= now && c.Cur_DateEnd >= now)
                .Select(c => c.Cur_Abbreviation.ToUpper())
                .ToHashSet();

            var expiry = DateTime.Today.AddDays(1) - DateTime.UtcNow;
            _cache.Set(CacheKey, activeCodes, expiry);

            return activeCodes;
        }
    }
}
