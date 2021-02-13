using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Models;
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

        public async Task<TransactionView> SaveTransaction(TransactionView transaction)
        {
            try
            {
                var transactionEntity = _context.Transactions
                    .Single(t => t.Id == transaction.Id);

                transactionEntity.Amount = transaction.Amount;
                transactionEntity.Date = transaction.Date;

                transactionEntity.RecordedReference = transaction.RecordedReference;
                transactionEntity.RecordedContact = transaction.RecordedContact;

                transactionEntity.ReferenceId = transaction.Reference?.Id ?? null;
                transactionEntity.ContactId = transaction.Contact?.Id ?? null;

                _context.Entry(transactionEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                transactionEntity = await _context.Transactions
                   .Include(t => t.Contact)
                   .Include(t => t.Reference)
                   .SingleAsync(t => t.Id == transaction.Id);

                return new TransactionView(transactionEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
