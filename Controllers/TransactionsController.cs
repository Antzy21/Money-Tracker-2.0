using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Models;
using MoneyTracker2.Data.DataAccessLayers;
using System;
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
        private TransactionRepository _transactionRepo;

        public TransactionsController(BankContext context, TransactionRepository transactionRepo)
        {
            _context = context;
            _transactionRepo = transactionRepo;
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
        public async Task<ActionResult<TransactionView>> PutTransaction(int id, TransactionView transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            try
            {
                var savedTransaction = await _transactionRepo.SaveTransaction(transaction);
                return new JsonResult(savedTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
