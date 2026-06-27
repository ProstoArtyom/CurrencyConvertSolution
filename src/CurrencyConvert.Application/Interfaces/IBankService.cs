using CurrencyConvert.Application.DTOs;

namespace CurrencyConvert.Application.Interfaces
{
    public interface IBankService
    {
        List<BankDto> GetBanks();
    }
}
