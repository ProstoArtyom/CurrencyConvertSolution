using CurrencyConvert.Application.DTOs;
using CurrencyConvert.Application.Interfaces;
using CurrencyConvert.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvert.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("convert")]
        public async Task<ActionResult<decimal>> ConvertFrom([FromQuery] ConvertRequestDto request, CancellationToken ct)
        {
            var bankCode = Enum.Parse<BankCode>(request.Bank, ignoreCase: true);

            var result = await _currencyService.ConvertAsync(
                fromCurrency: request.From.ToUpper(),
                toCurrency: request.To.ToUpper(),
                amount: request.Amount,
                bankCode: bankCode,
                ct: ct);

            return Ok(new ConvertResponseDto
            {
                From = request.From.ToUpper(),
                To = request.To.ToUpper(),
                Amount = request.Amount,
                Bank = bankCode.ToString(),
                Result = result
            });
        }
    }
}
