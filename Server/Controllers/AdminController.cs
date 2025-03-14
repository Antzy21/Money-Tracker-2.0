using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker2.Data;

namespace MoneyTracker2.Controllers;

[Route("[controller]")]
[ApiController]
public class AdminController(MoneyTrackerContext context) : ControllerBase
{
    [HttpDelete("DeleteAllTransactions")]
    public async Task<ActionResult> DeleteTransactions()
    {
        context.Transactions.RemoveRange(context.Transactions);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("DeleteAllCategories")]
    public async Task<ActionResult> DeleteAllCategories()
    {
        context.Categories.RemoveRange(context.Categories);
        await context.SaveChangesAsync();
        return Ok();
    }
}
