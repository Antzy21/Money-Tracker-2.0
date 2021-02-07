using System;

namespace MoneyTracker.Models
{
    public class TransactionView
    {
        public TransactionView(Transaction transaction)
        {
            Id = transaction.Id;
            Date = transaction.Date;
            Amount = transaction.Amount;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
    }
}
