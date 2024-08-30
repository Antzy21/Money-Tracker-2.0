using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker2.Services;

namespace MoneyTracker2.Controllers;

[Route("[controller]")]
[ApiController]
public class UploadController(TransactionImportService transactionImportService) : ControllerBase
{
    [HttpPost]
    public ActionResult Post(IFormFile file)
    {
        if (file == null)
        {
            return new BadRequestResult();
        }

        var newlyAddedTransactions = transactionImportService.ImportTransactionsFromFile(file).ToList();

        return new JsonResult(newlyAddedTransactions);
    }
}