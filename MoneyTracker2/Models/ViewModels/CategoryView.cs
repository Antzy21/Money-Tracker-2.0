using MoneyTracker2.Models.EntityModels;

namespace MoneyTracker2.Models.ViewModels
{
    public class CategoryView
    {
        public CategoryView(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Colour = category.Colour;
            ParentCategoryId = category.ParentCategoryId ?? 0;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public int ParentCategoryId { get; set; }

    }
}