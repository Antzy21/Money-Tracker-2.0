using System;

namespace MoneyTracker.Models
{
    public class TransactionView
    {
        public TransactionView()
        {
        }

        public TransactionView(Transaction transaction)
        {
            Id = transaction.Id;
            Date = transaction.Date;
            Amount = transaction.Amount;

            RecordedContact = transaction.Contact;
            RecordedReference = transaction.Reference;

            if (transaction.CategoryId != null)
            {
                Category = new CategoryView(transaction.Category);
            }
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public string RecordedContact { get; set; }
        public string RecordedReference { get; set; }

        public CategoryView Category { get; set; }
    }
}
