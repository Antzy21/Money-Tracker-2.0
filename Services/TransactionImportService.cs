using Microsoft.AspNetCore.Http;
using MoneyTracker.Data;
using MoneyTracker.Models;
using System.Collections.Generic;
using System.Linq;

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

        public IList<Transaction> ImportTransactionsFromFile(IFormFile file)
        {
            var csvTransactions = _csvService.ReadCsvTo<CsvTransaction>(file, LongLinePolicy.IncludeInLastLine);

            var newTransactions = new List<Transaction>();

            foreach (var ct in csvTransactions)
            {
                var transaction = new Transaction
                {
                    Amount = ct.Amount,
                    Date = ct.Date,
                };

                var additionalInfo = ct.Memo.Split("  ");
                additionalInfo = additionalInfo.Where(a => a.Length > 0).ToArray();

                transaction.RecordedContact = additionalInfo[0].Trim();
                transaction.RecordedCategory = additionalInfo[1].Trim();

                newTransactions.Add(transaction);
            }


            _context.Transactions.AddRange(newTransactions);

            _context.SaveChanges();

            var newTransactionIds = newTransactions.Select(nt => nt.Id).ToList();

            return _context.Transactions.Where(t => newTransactionIds.Contains(t.Id)).ToList();
        }

    }
}
