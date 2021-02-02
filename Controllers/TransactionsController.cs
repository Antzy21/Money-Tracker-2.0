using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Models;
using MoneyTracker.Services;
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
        private readonly CsvService _csvService;

        public TransactionsController(BankContext context, CsvService csvService)
        {
            _context = context;
            _csvService = csvService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions
                .Include(t => t.Contact)
                .Include(t => t.Reference)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Transaction>> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            var t = _context.Transactions
                .Include(t => t.Contact)
                .Include(t => t.Reference)
                .FirstOrDefault(t => t.Id == id);
            t = transaction;
            t.Reference = transaction.Reference;
            t.Contact = transaction.Contact;

            _context.Entry(transaction).State = EntityState.Modified;

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

            return await _context.Transactions.FindAsync(id);
        }

        public ActionResult ImportCsv()
        {
            var path = "CsvFiles\\test.csv";
            var csvTransactions = _csvService.ReadCsv(path);

            List<CsvTransaction> failedImports = new List<CsvTransaction>();

            foreach (var csvTransaction in csvTransactions)
            {
                try
                {
                    var transaction = new Transaction();

                    if (_context.Transactions.Any(t => t.Amount == transaction.Amount && t.Date == transaction.Date))
                    {
                        //model.AlreadyExistingTransactionCount += 1;
                    }
                    else
                    {
                        _context.Add(transaction);
                        //model.NewTransactions.Add(transaction);
                    }
                }
                catch
                {
                    //model.FailedImports.Add(csvTransaction);
                }
            }

            _context.SaveChangesAsync();

            return new JsonResult("ImportSuccess!");
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
