using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Enums;
using System.Net.Http.Json;

namespace CurrencyConvert.Infrastructure.Providers.Nbrb
{
    public class NbrbRateProvider : IBankRateProvider
    {
        public BankCode BankCode => BankCode.Nbrb;

        private readonly HttpClient _httpClient;
        public NbrbRateProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(NbrbRateProvider));
        }

        public async Task<decimal> GetRateAsync(string currencyCode, CancellationToken ct)
        {
            var response = await _httpClient.GetFromJsonAsync<NbrbRateResponse>(
                $"exrates/rates/{currencyCode}", ct);

            return response!.Cur_OfficialRate;
        }
    }
}
