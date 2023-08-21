using VirtualMind.Test.Library.Model;

namespace VirtualMind.Test.Library.Contracts
{
    public interface ITransactionRepository
    {
        Task<decimal> CanPurchase(int userId, string currencyCode);
        Task<Transaction> CreateTransaction(int userId, string currencyCode, decimal amountInPeso, decimal purchasedAmount);
    }
}
