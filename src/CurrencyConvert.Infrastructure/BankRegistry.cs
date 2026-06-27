using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Entities;
using CurrencyConvert.Domain.Enums;

namespace CurrencyConvert.Infrastructure
{
    public class BankRegistry : IBankRegistry
    {
        private static readonly IReadOnlyList<Bank> _banks =
        [
            new()
            {
                Code = BankCode.Nbrb,
                Name = "Национальный банк Республики Беларусь",
                ApiBaseUrl = "https://api.nbrb.by"
            },
            new()
            {
                Code = BankCode.AlfaBank,
                Name = "Альфа-Банк",
                ApiBaseUrl = "https://developerhub.alfabank.by"
            }
        ];

        public IReadOnlyList<Bank> GetAll() => _banks;
    }
}
