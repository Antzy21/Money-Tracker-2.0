using Microsoft.AspNetCore.Mvc;
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
        private readonly TransactionRepository _transactionRepo;

        public TransactionsController(TransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionView>>> Get()
        {
            var transactions = await _transactionRepo.GetTransactions();

            return new JsonResult(transactions.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionView>> GetTransaction(int id)
        {
            var transaction = await _transactionRepo.GetTransaction(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TransactionView>> PutTransaction(int id, TransactionView transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            var savedTransaction = await _transactionRepo.SaveTransaction(transaction);
            return new JsonResult(savedTransaction);
        }
    }
}
