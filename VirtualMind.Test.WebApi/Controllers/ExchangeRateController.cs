using Microsoft.AspNetCore.Mvc;
using VirtualMind.Test.Contracts.ServiceLibrary;

namespace VirtualMind.Test.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {

        private readonly IExternalCurrencyService _exchangeRateService;

        public ExchangeRateController(IExternalCurrencyService exchangeRateService) 
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("{currencyCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> GetExchangeRate(string currencyCode)
        {
            var rate = await _exchangeRateService.GetExchangeRate(currencyCode);
            if (rate == null)
            {
                return BadRequest("Invalid currency code.");
            }
            return Ok(new { Rate = rate });
        }
    }
}
