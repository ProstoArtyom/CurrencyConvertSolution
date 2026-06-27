using CurrencyConvert.Application.DTOs;
using CurrencyConvert.Application.Interfaces;

namespace CurrencyConvert.Application.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRegistry _bankRegistry;
        public BankService(IBankRegistry bankRegistry)
        {
            _bankRegistry = bankRegistry;
        }

        public List<BankDto> GetBanks() =>
            _bankRegistry
                .GetAll()
                .Select(b => new BankDto
                {
                    Code = b.Code,
                    Name = b.Name
                })
                .ToList();
    }
}
