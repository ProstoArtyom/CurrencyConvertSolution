using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Entities;
using CurrencyConvert.Domain.Enums;
using CurrencyConvert.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace CurrencyConvert.Infrastructure.Banks
{
    public class BankRegistry : IBankRegistry
    {
        private readonly IReadOnlyList<Bank> _banks;
        public BankRegistry(IOptions<BankSettings> options)
        {
            var bankSettings = options.Value;
            _banks =
            [
                new()
                {
                    Code = BankCode.Nbrb,
                    Name = bankSettings.Nbrb.Name
                },
                new()
                {
                    Code = BankCode.AlfaBank,
                    Name = bankSettings.Alfabank.Name
                }
            ];
        }

        public IReadOnlyList<Bank> GetAll() => _banks;
    }
}
