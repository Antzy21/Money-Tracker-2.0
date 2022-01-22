using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Models;
using MoneyTracker2.Data.DataAccessLayers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BankContext _context;
        private readonly TransactionRepository _transactionRepository;

        public CategoryController(BankContext context, TransactionRepository transactionRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryView>>> GetCategories()
        {
            return await _context.Categories.Include(r => r.Transactions)
                .Select(r => new CategoryView(r)).ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryView>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return new CategoryView(category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> LinkCategory(LinkCategoryObject linkCategory)
        {
            if (linkCategory.recordedCategory == null || linkCategory.category == null)
            {
                return BadRequest();
            }

            var updatedTransactionIds = await _transactionRepository.LinkCategoryAsync(linkCategory.recordedCategory, linkCategory.category);

            var updatedTransactions = await _transactionRepository.GetTransactions(updatedTransactionIds);

            return new JsonResult(updatedTransactions.ToList());
        }
        public class LinkCategoryObject
        {
            public string recordedCategory { get; set; }
            public CategoryView category { get; set; }
        }

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
