using Microsoft.AspNetCore.Http;
using MoneyTracker.Data;
using MoneyTracker.Models;

namespace MoneyTracker.Services
{
    public class TransactionImportService
    {
        private readonly CsvService _csvService = new CsvService();
        private readonly BankContext _context;

        public TransactionImportService(BankContext context)
        {
            _context = context;
        }

        public void ImportTransactionsFromFile(IFormFile file)
        {
            var csvTransactions = _csvService.ReadCsvTo<CsvTransaction>(file);
        }
    }
}
