using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyTracker2.Data.DataAccessLayers
{
    public class TransactionRepository
    {
        private readonly BankContext _context;

        public TransactionRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<TransactionView> GetTransaction(int id)
        {
            var transactionEntity = await _context.Transactions
                .Include(t => t.Category)
                .SingleOrDefaultAsync(t => t.Id == id);

            return new TransactionView(transactionEntity);
        }
        public async Task<IList<TransactionView>> GetTransactions()
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Select(t => new TransactionView(t))
                .ToListAsync();
        }
        public async Task<IList<TransactionView>> GetTransactions(List<int> ids)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Where(t => ids.Contains(t.Id))
                .Select(t => new TransactionView(t))
                .ToListAsync();
        }

        public async Task<TransactionView> SaveTransaction(TransactionView transaction)
        {
            try
            {
                var transactionEntity = _context.Transactions
                    .Single(t => t.Id == transaction.Id);

                transactionEntity.Amount = transaction.Amount;
                transactionEntity.Date = transaction.Date;

                transactionEntity.Reference = transaction.RecordedReference;
                transactionEntity.Contact = transaction.RecordedContact;

                transactionEntity.CategoryId = transaction.Category?.Id ?? null;

                _context.Entry(transactionEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                transactionEntity = await _context.Transactions
                   .Include(t => t.Category)
                   .SingleAsync(t => t.Id == transaction.Id);

                return new TransactionView(transactionEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<List<int>> LinkReferenceAsync(string recordedReference, CategoryView category)
        {
            try
            {
                var transactions = await _context.Transactions.Where(t => t.Reference == recordedReference).ToListAsync();

                foreach (var transaction in transactions)
                {
                    transaction.CategoryId = category.Id;
                }

                await _context.SaveChangesAsync();

                return transactions.Select(t => t.Id).ToList();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<List<int>> LinkCategoryAsync(string recordedContact, CategoryView category)
        {
            try
            {
                var transactions = await _context.Transactions.Where(t => t.Contact == recordedContact).ToListAsync();

                foreach (var transaction in transactions)
                {
                    transaction.CategoryId = category.Id;
                }

                await _context.SaveChangesAsync();

                return transactions.Select(t => t.Id).ToList();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
