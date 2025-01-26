using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker2.Models.EntityModels;

public class CategoryRegex
{
    [Key]
    public string Regex { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}