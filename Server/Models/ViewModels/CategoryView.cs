using System.Collections.Generic;

namespace MoneyTracker2.Models.ViewModels;

public class CategoryView
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Colour { get; set; }
    public List<string> Regexes { get; set; } = [];
    public int? ParentCategoryId { get; set; }
}