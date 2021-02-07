using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Services;

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

            _transactionImportService.ImportTransactionsFromFile(file);

            return new JsonResult("ImportSuccess!");
        }
    }
}
