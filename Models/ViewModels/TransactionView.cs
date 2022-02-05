using System;

namespace MoneyTracker.Models.ViewModels
{
    public class TransactionView
    {
        public TransactionView(Transaction transaction)
        {
            Id = transaction.Id;
            Date = transaction.Date;
            Amount = transaction.Amount;

            Contact = transaction.Contact;
            Reference = transaction.Reference;

            if (transaction.CategoryId != null)
            {
                Category = new CategoryView(transaction.Category);
            }
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public string Contact { get; set; }
        public string Reference { get; set; }

        public CategoryView Category { get; set; }
    }
}
