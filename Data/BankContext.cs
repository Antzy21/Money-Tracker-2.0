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
    }
}