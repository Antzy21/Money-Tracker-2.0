using System.Collections.Generic;

namespace MoneyTracker.Models.ViewModels
{
    public class CategoryNodeView : CategoryView
    {
        public CategoryNodeView(Category category) : base(category)
        {
        }

        public List<CategoryNodeView> nodes = new();
    }
}
