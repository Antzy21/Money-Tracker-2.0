using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public int SortCode { get; set; }
        public int Account { get; set; }
        public float Amount { get; set; }
        public string Subcategory { get; set; }
        public string TransactionType { get; set; }

        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        public int ReferenceId { get; set; }
        [ForeignKey("ReferenceId")]
        public Reference Reference { get; set; }
    }
}
