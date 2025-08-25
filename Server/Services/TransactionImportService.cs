using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MoneyTracker2.Data;
using MoneyTracker2.Models.EntityModels;
using MoneyTracker2.Models.External;
using MoneyTracker2.Models.Responses;

namespace MoneyTracker2.Services;

public class TransactionImportService(MoneyTrackerContext context)
{
    private readonly CsvService _csvService = new CsvService();

    public TransactionUploadResponse ImportTransactionsFromFile(IFormFile file)
    {
        var csvTransactions = _csvService.ReadCsvTo<CsvTransaction>(file, LongLinePolicy.IncludeInLastLine);

        var newTransactions = new List<Transaction>();

        foreach (var ct in csvTransactions)
        {
            var additionalInfo = ct.Memo.Split("  ");
            additionalInfo = additionalInfo.Where(a => a.Length > 0).ToArray();

            var transaction = new Transaction
            {
                Amount = ct.Amount,
                Date = ct.Date,
                Contact = additionalInfo[0].Trim(),
                Reference = additionalInfo[1].Trim(),
            };

            newTransactions.Add(transaction);
        }

        var duplicates = newTransactions
            .Where(nt => context.Transactions.Any(t =>
                t.Date == nt.Date &&
                t.Amount == nt.Amount &&
                t.Contact == nt.Contact &&
                t.Reference == nt.Reference
            ))
            .ToList();

        var uniqueNew = newTransactions.Except(duplicates).ToList();

        context.Transactions.AddRange(uniqueNew);

        context.SaveChanges();

        var newTransactionIds = newTransactions.Select(nt => nt.Id).ToList();

        return new TransactionUploadResponse(
            Transactions: context.Transactions.Where(t => newTransactionIds.Contains(t.Id)).ToList(),
            DuplicatesCount: duplicates.Count
        );
    }
}