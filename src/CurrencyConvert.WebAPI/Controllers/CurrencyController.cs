using CurrencyConvert.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvert.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IBankService _bankService;
        public CurrencyController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet("banks")]
        public async Task<IActionResult> GetBanks()
        {
            var banks = _bankService.GetBanks();
            return Ok(banks);
        }
    }
}
