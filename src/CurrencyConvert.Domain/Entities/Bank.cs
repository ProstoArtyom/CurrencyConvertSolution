using CurrencyConvert.Domain.Enums;

namespace CurrencyConvert.Domain.Entities
{
    public class Bank
    {
        public BankCode Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ApiBaseUrl { get; set; } = string.Empty;
    }
}