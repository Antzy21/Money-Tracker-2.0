using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }

        public int? ContactGroupId { get; set; }
        [ForeignKey("ContactGroupId")]
        public ContactGroup ContactGroup { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}