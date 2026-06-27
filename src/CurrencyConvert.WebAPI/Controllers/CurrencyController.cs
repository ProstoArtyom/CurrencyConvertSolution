using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvert.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        [HttpGet("banks")]
        public async Task<IActionResult> GetBanks()
        {
            return Ok();
        }
    }
}
