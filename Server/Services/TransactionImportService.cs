using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MoneyTracker2.Data;
using MoneyTracker2.Models.EntityModels;
using MoneyTracker2.Models.External;

namespace MoneyTracker2.Services;

public class TransactionImportService(MoneyTrackerContext context)
{
    private readonly CsvService _csvService = new CsvService();

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

            transaction.Contact = additionalInfo[0].Trim();
            transaction.Reference = additionalInfo[1].Trim();

            newTransactions.Add(transaction);
        }

        context.Transactions.AddRange(newTransactions);

        context.SaveChanges();

        var newTransactionIds = newTransactions.Select(nt => nt.Id).ToList();

        return context.Transactions.Where(t => newTransactionIds.Contains(t.Id)).ToList();
    }
}