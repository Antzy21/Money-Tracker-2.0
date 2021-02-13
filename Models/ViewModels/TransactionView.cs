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

            RecordedContact = transaction.RecordedContact;
            RecordedReference = transaction.RecordedReference;

            if (transaction.Contact != null)
            {
                Contact = new ContactView(transaction.Contact);
            }
            if (transaction.Reference != null)
            {
                Reference = new ReferenceView(transaction.Reference);
            }
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public string RecordedContact { get; set; }
        public string RecordedReference { get; set; }

        public ContactView Contact { get; set; }
        public ReferenceView Reference { get; set; }
    }
}
