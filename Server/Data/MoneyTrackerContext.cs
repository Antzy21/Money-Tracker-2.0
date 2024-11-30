using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoneyTracker2.Models.EntityModels;

namespace MoneyTracker2.Data;

public class MoneyTrackerContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }

    private string _dbPath { get; }

    public MoneyTrackerContext(IConfiguration configuration)
    {
        var appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var environmentVariablePath = configuration.GetValue<string>("DatabasePath");
        var databasePath = Path.Combine(appdataPath, "Antzy21", "MoneyTracker", environmentVariablePath);
        
        Directory.CreateDirectory(databasePath);
        _dbPath = Path.Join(databasePath, "moneyTracker.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}");
}