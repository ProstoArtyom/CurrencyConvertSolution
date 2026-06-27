using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Entities;

namespace CurrencyConvert.Infrastructure
{
    public class BankRegistry : IBankRegistry
    {
        private static readonly IReadOnlyList<Bank> _banks =
        [
            new()
            {
                Code = "NBRB",
                Name = "Национальный банк Республики Беларусь",
                ApiBaseUrl = "https://api.nbrb.by"
            },
            new()
            {
                Code = "ALFABANK",
                Name = "Альфа-Банк",
                ApiBaseUrl = "https://developerhub.alfabank.by"
            }
        ];

        public IReadOnlyList<Bank> GetAll() => _banks;
    }
}
