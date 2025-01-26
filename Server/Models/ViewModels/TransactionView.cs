using System;

namespace MoneyTracker2.Models.ViewModels;

public class TransactionView
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public float Amount { get; set; }
    public required string Contact { get; set; }
    public required string Reference { get; set; }

    public CategoryView? Category { get; set; }
}