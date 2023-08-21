using VirtualMind.Test.Contracts.ServiceLibrary;
using VirtualMind.Test.Library.Contracts;
using VirtualMind.Test.Library.Model;

namespace VirtualMind.Test.Impl.ServiceLibrary
{
    public class TransactionService : ITransactionService
    {


        private const decimal DOLLAR_MONTHLY_LIMIT = 200;
        private const decimal REAL_MONTHLY_LIMIT = 300;

        private readonly ITransactionRepository _transactionRepository;
        private readonly IExternalCurrencyService _externalCurrencyService;
        public TransactionService(ITransactionRepository transactionRepository, IExternalCurrencyService externalCurrencyService) 
        {
            _transactionRepository = transactionRepository;
            _externalCurrencyService = externalCurrencyService;
        }

        public async Task<decimal> CalculatePurchasedAmount(string currencyCode, decimal amountInPeso)
        {
            var exchangeRate = await _externalCurrencyService.GetExchangeRate(currencyCode) ?? 0;
            return amountInPeso / exchangeRate;
        }

        public async Task<bool> CanPurchase(int userId, string currencyCode, decimal amountInPeso)
        {
            decimal monthlyLimitInForeignCurrency = GetMonthlyLimit(currencyCode);

            decimal currentMonthPurchasesInForeignCurrency = await _transactionRepository.CanPurchase(userId, currencyCode);

            var exchangeRate = await _externalCurrencyService.GetExchangeRate(currencyCode) ?? 0;

            decimal proposedPurchaseInForeignCurrency = amountInPeso / exchangeRate;

            return (currentMonthPurchasesInForeignCurrency + proposedPurchaseInForeignCurrency) <= monthlyLimitInForeignCurrency;
        }

        public async Task<Transaction> CreateTransaction(int userId, string currencyCode, decimal amountInPeso)
        {
            var exchangeRate = await _externalCurrencyService.GetExchangeRate(currencyCode) ?? 0;

            decimal purchasedAmount = amountInPeso / exchangeRate;

            var transaction = await _transactionRepository.CreateTransaction(userId, currencyCode, amountInPeso, purchasedAmount);
            
            return transaction;
        }


        private decimal GetMonthlyLimit(string currencyCode)
        {
            switch (currencyCode.ToUpper())
            {
                case "USD":
                    return DOLLAR_MONTHLY_LIMIT;
                case "BRL":
                    return REAL_MONTHLY_LIMIT;
                default:
                    throw new ArgumentException("Invalid currency code");
            }
        }
    }
}