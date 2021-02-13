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
    public class ReferencesController : ControllerBase
    {
        private readonly BankContext _context;
        private readonly TransactionRepository _transactionRepository;

        public ReferencesController(BankContext context, TransactionRepository transactionRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
        }

        // GET: api/References
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferenceView>>> GetReferences()
        {
            return await _context.References.Include(r => r.Transactions)
                .Select(r => new ReferenceView(r)).ToListAsync();
        }

        // GET: api/References/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReferenceView>> GetReference(int id)
        {
            var reference = await _context.References.FindAsync(id);

            if (reference == null)
            {
                return NotFound();
            }

            return new ReferenceView(reference);
        }

        // PUT: api/References/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReference(int id, Reference reference)
        {
            if (id != reference.Id)
            {
                return BadRequest();
            }

            _context.Entry(reference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenceExists(id))
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
        public async Task<IActionResult> LinkReference(LinkReferenceObject linkReference)
        {
            if (linkReference.recordedReference == null || linkReference.reference == null)
            {
                return BadRequest();
            }

            var updatedTransactionIds = await _transactionRepository.LinkReferenceAsync(linkReference.recordedReference, linkReference.reference);

            var updatedTransactions = await _transactionRepository.GetTransactions(updatedTransactionIds);

            return new JsonResult(updatedTransactions.ToList());
        }
        public class LinkReferenceObject
        {
            public string recordedReference { get; set; }
            public ReferenceView reference { get; set; }
        }

        // POST: api/References
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reference>> PostReference(Reference reference)
        {
            _context.References.Add(reference);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReference", new { id = reference.Id }, reference);
        }

        // DELETE: api/References/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reference>> DeleteReference(int id)
        {
            var reference = await _context.References.FindAsync(id);
            if (reference == null)
            {
                return NotFound();
            }

            _context.References.Remove(reference);
            await _context.SaveChangesAsync();

            return reference;
        }

        private bool ReferenceExists(int id)
        {
            return _context.References.Any(e => e.Id == id);
        }
    }
}
