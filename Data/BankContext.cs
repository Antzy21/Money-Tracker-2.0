using Microsoft.EntityFrameworkCore;
using MoneyTracker.Models;

namespace MoneyTracker.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<ContactGroup> ContactGroups { get; set; }
        public DbSet<ReferenceGroup> ReferenceGroups { get; set; }
    }
}