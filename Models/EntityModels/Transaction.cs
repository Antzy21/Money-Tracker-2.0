using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }

        public int ContactId { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        public int ReferenceId { get; set; }
        [ForeignKey("ReferenceId")]
        public Reference Reference { get; set; }
    }
}
