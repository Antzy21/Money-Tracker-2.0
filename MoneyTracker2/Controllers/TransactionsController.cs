using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker2.Data.DataAccessLayers;
using MoneyTracker2.Models.ViewModels;

namespace MoneyTracker2.Controllers;

[Route("[controller]")]
[ApiController]
public class TransactionsController(TransactionRepository transactionRepo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionView>>> Get()
    {
        var transactions = await transactionRepo.GetTransactions();
        return new JsonResult(transactions.ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionView>> GetTransaction(int id)
    {
        var transaction = await transactionRepo.GetTransaction(id);
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
        var savedTransaction = await transactionRepo.SaveTransaction(transaction);
        return new JsonResult(savedTransaction);
    }
}