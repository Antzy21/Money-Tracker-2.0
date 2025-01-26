using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker2.Models.EntityModels;

public class Category
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Colour { get; set; }

    public int? ParentCategoryId { get; set; }
    [ForeignKey("ParentCategoryId")]
    public Category? ParentCategory { get; set; }

    public required List<CategoryRegex> Regexes { get; set; }

    public required List<Category> ChildCategories { get; set; }

    public required List<Transaction> Transactions { get; set; }
}