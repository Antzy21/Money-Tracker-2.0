using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyTracker2.Models.ViewModels;

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
            var transactionEntity = _context.Transactions
                .Single(t => t.Id == transaction.Id);

            transactionEntity.Amount = transaction.Amount;
            transactionEntity.Date = transaction.Date;
            transactionEntity.Reference = transaction.Reference;
            transactionEntity.Contact = transaction.Contact;
            transactionEntity.CategoryId = transaction.Category?.Id;

            _context.Entry(transactionEntity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return await GetTransaction(transactionEntity.Id);
        }

        public async Task<List<int>> LinkCategoryAsync(string contact, CategoryView category)
        {
            var transactions = await _context.Transactions
                .Where(t => t.Contact == contact)
                .ToListAsync();

            foreach (var transaction in transactions)
            {
                transaction.CategoryId = category.Id;
            }

            await _context.SaveChangesAsync();

            return transactions.Select(t => t.Id).ToList();
        }
    }
}
