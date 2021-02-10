using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTracker.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly BankContext _context;

        public TransactionsController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionView>>> Get()
        {
            return await _context.Transactions
                .Include(t => t.Contact)
                .Include(t => t.Reference)
                .Select(t => new TransactionView(t))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionView>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Contact)
                .Include(t => t.Reference)
                .SingleOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return new TransactionView(transaction);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Transaction>> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            var transactionEntity = _context.Transactions
                .FirstOrDefault(t => t.Id == id);

            transactionEntity.Amount = transaction.Amount;
            transactionEntity.Date = transaction.Date;

            transactionEntity.RecordedReference = transaction.RecordedReference;
            transactionEntity.RecordedContact = transaction.RecordedContact;

            transactionEntity.Reference = transaction.Reference;
            transactionEntity.Contact = transaction.Contact;

            _context.Entry(transactionEntity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(new TransactionView(transactionEntity));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
