using Microsoft.EntityFrameworkCore;
using MoneyTracker2.Models.EntityModels;

namespace MoneyTracker2.Data;

public class BankContext(DbContextOptions<BankContext> options) : DbContext(options)
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
}