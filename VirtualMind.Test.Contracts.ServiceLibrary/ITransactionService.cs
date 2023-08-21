
using VirtualMind.Test.Library.Model;

namespace VirtualMind.Test.Contracts.ServiceLibrary
{
    public  interface ITransactionService
    {
        Task<bool> CanPurchase(int userId, string currencyCode, decimal amountInPeso);
        Task<decimal> CalculatePurchasedAmount(string currencyCode, decimal amountInPeso);
        Task<Transaction> CreateTransaction(int userId, string currencyCode, decimal amountInPeso);

    }
}
