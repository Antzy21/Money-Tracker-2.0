using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker2.Services;

namespace MoneyTracker2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly TransactionImportService _transactionImportService;

        public UploadController(TransactionImportService transactionImportService)
        {
            _transactionImportService = transactionImportService;
        }

        [HttpPost]
        public ActionResult Post(IFormFile file)
        {
            if (file == null)
            {
                return new JsonResult("Failed");
            }

            var newlyAddedTransactions = _transactionImportService.ImportTransactionsFromFile(file).ToList();

            return new JsonResult(newlyAddedTransactions);
        }
    }
}
