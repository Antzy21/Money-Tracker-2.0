using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker2.Data.DataAccessLayers;
using MoneyTracker2.Models.EntityModels;
using MoneyTracker2.Models.ViewModels;

namespace MoneyTracker2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepo;

        public CategoriesController(CategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryView>>> Get()
        {
            var categories = await _categoryRepo.GetCategories();
            
            return new JsonResult(categories.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryView>> GetCategory(int id)
        {
            var category = await _categoryRepo.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryView category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            var savedCategory = await _categoryRepo.SaveCategory(category);
            return new JsonResult(savedCategory);
        }

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryView category)
        {
            var savedCategory = await _categoryRepo.SaveCategory(category);

            return CreatedAtAction("GetCategory", new { id = savedCategory.Id }, category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryView>> DeleteCategory(int id)
        {
            var category = await _categoryRepo.DeleteCategory(id);
            return category;
        }
    }
}
