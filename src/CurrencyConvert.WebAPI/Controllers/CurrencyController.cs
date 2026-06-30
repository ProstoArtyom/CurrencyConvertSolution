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

        /// <summary>
        /// Convert an amount from one currency to another using the selected bank's exchange rate.
        /// </summary>
        /// <param name="request">Source currency, target currency, amount, and bank code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Conversion result including the applied rate</returns>
        [HttpGet("convert")]
        public async Task<ActionResult<ConvertResponseDto>> Convert(
            [FromQuery] ConvertRequestDto request,
            CancellationToken ct)
        {
            var bankCode = Enum.Parse<BankCode>(request.Bank, ignoreCase: true);

            var convertResponseDto = await _currencyService.ConvertAsync(
                fromCurrency: request.From.ToUpper(),
                toCurrency: request.To.ToUpper(),
                amount: request.Amount,
                bankCode: bankCode,
                ct: ct);

            return Ok(convertResponseDto);
        }

        /// <summary>
        /// Get the exchange rate of a currency against BYN from the selected bank.
        /// </summary>
        /// <param name="request">Currency code and bank code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Buy and sell rate for the currency from the selected bank</returns>
        [HttpGet("rates")]
        public async Task<ActionResult<RateResultDto>> GetRate(
            [FromQuery] RateRequestDto request,
            CancellationToken ct)
        {
            var bankCode = Enum.Parse<BankCode>(request.Bank, ignoreCase: true);

            var result = await _currencyService.GetRateAsync(
                currency: request.Currency.ToUpper(),
                bankCode: bankCode,
                ct: ct);

            return Ok(result);
        }
    }
}
