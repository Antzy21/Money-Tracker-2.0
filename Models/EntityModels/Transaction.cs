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
        public string RecordedContact { get; set; }
        public string RecordedCategory { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
