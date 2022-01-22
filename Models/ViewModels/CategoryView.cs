namespace MoneyTracker.Models
{
    public class CategoryView
    {
        public CategoryView()
        {
        }

        public CategoryView(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Colour = category.Colour;
            CategoryParentName = category.ParentCategory?.Name ?? "None";
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string CategoryParentName { get; set; }

    }
}