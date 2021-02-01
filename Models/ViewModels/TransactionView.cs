using System;

namespace MoneyTracker.Models
{
    public class TransactionView
    {
        public TransactionView(Transaction transaction)
        {
            Id = transaction.Id;
            Number = transaction.Number;
            Date = transaction.Date;
            SortCode = transaction.SortCode;
            Account = transaction.Account;
            Amount = transaction.Amount;
            Subcategory = transaction.Subcategory;
            TransactionType = transaction.TransactionType;
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public int SortCode { get; set; }
        public int Account { get; set; }
        public float Amount { get; set; }
        public string Subcategory { get; set; }
        public string TransactionType { get; set; }
    }
}
