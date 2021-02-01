using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Models
{
    public class ReferenceGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public List<Reference> References { get; set; }
    }
}