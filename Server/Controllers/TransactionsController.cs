using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyTracker2.Data;
using MoneyTracker2.Models.ViewModels;

namespace MoneyTracker2.Controllers;

[Route("[controller]")]
[ApiController]
public class TransactionsController(MoneyTrackerContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionView>>> Get()
    {
        var categories = await context.Categories
            .Include(c => c.Regexes)
            .ToListAsync();

        var transactions = await context.Transactions
            .Select(t => new TransactionView
            {
                Id = t.Id,
                Date = t.Date,
                Amount = t.Amount,
                Contact = t.Contact,
                Reference = t.Reference,
            })
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        foreach (var transaction in transactions)
        {
            transaction.Categories = categories
                .Where(c => c.Regexes.Any(r => {
                    var regex = new Regex(r.Regex, RegexOptions.IgnoreCase);
                    return regex.IsMatch(transaction.Reference) || regex.IsMatch(transaction.Contact);
                }))
                .Select(c => new CategoryView()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Colour = c.Colour
                })
                .ToList();
        }

        return new JsonResult(transactions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionView>> GetTransaction(int id)
    {
        var transaction = await context.Transactions
            .SingleOrDefaultAsync(t => t.Id == id);

        if (transaction == null)
            return NotFound();

        return new TransactionView
        {
            Id = transaction.Id,
            Date = transaction.Date,
            Amount = transaction.Amount,
            Contact = transaction.Contact,
            Reference = transaction.Reference,
        };
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TransactionView>> PutTransaction(TransactionView transaction)
    {
        var transactionEntity = await context.Transactions
            .SingleOrDefaultAsync(t => t.Id == transaction.Id);

        if (transactionEntity is null)
            return NotFound();

        transactionEntity.Amount = transaction.Amount;
        transactionEntity.Date = transaction.Date;
        transactionEntity.Reference = transaction.Reference;
        transactionEntity.Contact = transaction.Contact;

        context.Entry(transactionEntity).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return Ok(transaction);
    }
}