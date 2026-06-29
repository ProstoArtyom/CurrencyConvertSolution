using CurrencyConvert.Application.DTOs;
using CurrencyConvert.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvert.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BanksController : ControllerBase
    {
        private readonly IBankService _bankService;
        public BanksController(IBankService bankService)
        {
            _bankService = bankService;
        }

        /// <summary>
        /// Get list of available banks
        /// </summary>
        /// <returns>List of available banks</returns>
        [HttpGet]
        public ActionResult<IEnumerable<BankDto>> GetBanks()
        {
            var banks = _bankService.GetBanks();
            return Ok(banks);
        }
    }
}
