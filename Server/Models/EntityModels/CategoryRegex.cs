using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker2.Models.EntityModels;

public class CategoryRegex
{
    [Key]
    public required string Regex { get; set; }

    [ForeignKey("CategoryId")]
    public required Category Category { get; set; }
}