using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTracker2.Data;
using MoneyTracker2.Models.EntityModels;
using MoneyTracker2.Models.ViewModels;

namespace MoneyTracker2.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(MoneyTrackerContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryView>>> Get()
    {
        var categories = await context.Categories
            .Include(c => c.Regexes)
            .Select(c => new CategoryView
            {
                Id = c.Id,
                Name = c.Name,
                Colour = c.Colour,
                Regexes = c.Regexes.Select(r => r.Regex).ToList(),
                ParentCategoryId = c.ParentCategoryId
            })
            .ToListAsync();
            
        return new JsonResult(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryView>> GetCategory(int id)
    {
        var category = await context.Categories
            .Include(c => c.Regexes)
            .SingleOrDefaultAsync(c => c.Id == id);
        
        if (category == null)
            return NotFound();
        
        return new CategoryView
        {
            Id = category.Id,
            Name = category.Name,
            Colour = category.Colour,
            Regexes = category.Regexes.Select(r => r.Regex).ToList(),
            ParentCategoryId = category.ParentCategoryId
        };
    }

    // POST: api/Categories
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory(CategoryView category)
    {
        if (category.Id == 0)
        {
            var newCategoryEntity = context.Categories.Add(new Category
            {
                Name = category.Name,
                Colour = category.Colour,
                ParentCategoryId = null,
                Regexes = [],
                ChildCategories = [],
                Transactions = []
            });

            await context.SaveChangesAsync();
            
            return CreatedAtAction("GetCategory", new { id = newCategoryEntity.Entity.Id }, newCategoryEntity.Entity);
        }

        var categoryEntity = await context.Categories
            .SingleOrDefaultAsync(t => t.Id == category.Id);

        if (categoryEntity is null)
            return NotFound();

        categoryEntity.Name = category.Name;
        categoryEntity.ParentCategoryId = category.ParentCategoryId;
        categoryEntity.Colour = category.Colour;
        
        context.Entry(categoryEntity).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        return Ok(category);
    }

    [HttpPost("AddRegex")]
    public async Task<ActionResult> AddCategoryRegex([FromBody] CategoryRegexView categoryRegexView)
    {
        var category = await context.Categories
            .Include(c => c.Regexes)
            .SingleOrDefaultAsync(c => c.Id == categoryRegexView.CategoryId);

        if (category == null)
            return NotFound();

        var categoryRegex = new CategoryRegex
        {
            Regex = categoryRegexView.Regex,
            Category = category
        };

        category.Regexes.Add(categoryRegex);
        await context.SaveChangesAsync();

        return Ok(categoryRegexView);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var categoryEntity = await context.Categories
            .SingleOrDefaultAsync(t => t.Id == id);

        if (categoryEntity is null)
            return NotFound();
        
        context.Categories.Remove(categoryEntity);
        await context.SaveChangesAsync();
        return Ok();
    }
}
