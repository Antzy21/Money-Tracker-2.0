namespace MoneyTracker2.Models.ViewModels;

public class CategoryView
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Colour { get; set; }
    public int? ParentCategoryId { get; set; }
}