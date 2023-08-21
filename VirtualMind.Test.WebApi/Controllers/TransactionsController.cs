using Microsoft.AspNetCore.Mvc;
using VirtualMind.Test.Contracts.ServiceLibrary;
using VirtualMind.Test.Contracts.ServiceLibrary.Params;

namespace VirtualMind.Test.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> Purchase( [FromBody] ParameterTransaction parameters)
        {
            if (!await _transactionService.CanPurchase(parameters.UserId, parameters.CurrencyCode, parameters.AmountInPeso))
            {
                return BadRequest("Purchase exceeds the monthly limit for the selected currency.");
            }

            var transaction = await _transactionService.CreateTransaction(parameters.UserId, parameters.CurrencyCode, parameters.AmountInPeso);

            return CreatedAtAction(nameof(Purchase), new { id = transaction.Id }, transaction);
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> GetExchangeRate(string currencyCode)
        {
            var rate = await _transactionService.CalculatePurchasedAmount(currencyCode, 1M);
            return Ok(rate); 
        }
    }
}
