using Microsoft.EntityFrameworkCore;
using VirtualMind.Test.EF.Context;
using VirtualMind.Test.Library.Contracts;
using VirtualMind.Test.Library.Model;

namespace VirtualMind.Test.EF.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context) 
        {
            _context = context;
        }
        
        public async Task<decimal> CanPurchase(int userId, string currencyCode)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId
                        && t.CurrencyCode == currencyCode
                        && t.TransactionDate.Month == DateTime.Now.Month)
                .SumAsync(t => t.PurchasedAmount);
        }

        public async Task<Transaction> CreateTransaction(int userId, string currencyCode, decimal amountInPeso, decimal purchasedAmount)
        {
            var transaction = new Transaction
            {
                UserId = userId,
                CurrencyCode = currencyCode,
                AmountInPeso = amountInPeso,
                PurchasedAmount = purchasedAmount
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}
