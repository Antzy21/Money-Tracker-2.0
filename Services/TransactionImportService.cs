using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public void ImportTransactionsFromFile(IFormFile file)
        {
            var csvTransactions = _csvService.ReadCsvTo<CsvTransaction>(file);

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
                transaction.RecordedReference = additionalInfo[1].Trim();

                newTransactions.Add(transaction);
            }

            CheckForExistingContacts(newTransactions);

            CheckForExistingReferences(newTransactions);

            _context.Transactions.AddRange(newTransactions);

            _context.SaveChanges();
        }

        private void CheckForExistingContacts(List<Transaction> newTransactions)
        {
            var newRecordedContacts = newTransactions.Select(t => t.RecordedContact);

            var matchingContacts = _context.Transactions
                .Include(t => t.Contact)
                .Where(t => newRecordedContacts.Contains(t.RecordedContact))
                .Select(t => new { RecordedContact = t.RecordedContact, Contact = t.Contact }).Distinct();

            foreach (var newTransaction in newTransactions)
            {
                var matchingContact = matchingContacts.SingleOrDefault(mc => newTransaction.RecordedContact == mc.RecordedContact);
                if (matchingContact != null)
                {
                    newTransaction.Contact = matchingContact.Contact;
                }
            }
        }
        private void CheckForExistingReferences(List<Transaction> newTransactions)
        {
            var newRecordedReferences = newTransactions.Select(t => t.RecordedReference);

            var matchingReferences = _context.Transactions
                .Include(t => t.Reference)
                .Where(t => newRecordedReferences.Contains(t.RecordedReference))
                .Select(t => new { RecordedReference = t.RecordedReference, Reference = t.Reference }).Distinct();

            foreach (var newTransaction in newTransactions)
            {
                var matchingReference = matchingReferences.SingleOrDefault(mc => newTransaction.RecordedReference == mc.RecordedReference);
                if (matchingReference != null)
                {
                    newTransaction.Reference = matchingReference.Reference;
                }
            }
        }
    }
}
