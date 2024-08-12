using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyTracker2.Models.EntityModels;
using MoneyTracker2.Models.ViewModels;

namespace MoneyTracker2.Data.DataAccessLayers
{
    public class CategoryRepository
    {
        private readonly BankContext _context;

        public CategoryRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<CategoryView> GetCategory(int id)
        {
            var categoryEntity = await _context.Categories
                .SingleOrDefaultAsync(c => c.Id == id);

            return new CategoryView(categoryEntity);
        }

        public async Task<IList<CategoryView>> GetCategories()
        {
            return await _context.Categories
                .Select(c => new CategoryView(c))
                .ToListAsync();
        }

        public async Task<IList<CategoryView>> GetCategories(List<int> ids)
        {
            return await _context.Categories
                .Where(c => ids.Contains(c.Id))
                .Select(c => new CategoryView(c))
                .ToListAsync();
        }

        public async Task<CategoryView> SaveCategory(CategoryView category)
        {
            var categoryEntity = _context.Categories
                .SingleOrDefault(t => t.Id == category.Id);

            if (categoryEntity is null)
            {
                _context.Categories.Add(new Category
                {
                    Name = category.Name,
                    ParentCategoryId = category.ParentCategoryId,
                    Colour = category.Colour,
                });
            }
            else
            {
                categoryEntity.Name = category.Name;
                categoryEntity.ParentCategoryId = category.ParentCategoryId;
                categoryEntity.Colour = category.Colour;

                _context.Entry(categoryEntity).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            return await GetCategory(category.Id);
        }

        public async Task<CategoryView> DeleteCategory(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(categoryEntity);
            await _context.SaveChangesAsync();
            return new CategoryView(categoryEntity);
        }
    }
}
