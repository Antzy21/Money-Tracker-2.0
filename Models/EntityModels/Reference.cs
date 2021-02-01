using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Models
{
    public class Reference
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }

        public int ReferenceGroupId { get; set; }
        [ForeignKey("ReferenceGroupId")]
        public ReferenceGroup ReferenceGroup { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}