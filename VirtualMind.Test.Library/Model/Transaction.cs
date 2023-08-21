using System.ComponentModel.DataAnnotations;

namespace VirtualMind.Test.Library.Model
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal AmountInPeso { get; set; }
        public decimal PurchasedAmount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;

    }
}
