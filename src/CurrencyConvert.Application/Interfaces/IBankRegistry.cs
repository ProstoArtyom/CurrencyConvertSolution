using CurrencyConvert.Domain.Entities;

namespace CurrencyConvert.Application.Interfaces
{
    public interface IBankRegistry
    {
        IReadOnlyList<Bank> GetAll();
    }
}
