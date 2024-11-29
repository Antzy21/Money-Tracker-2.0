using System;
using MoneyTracker2.Models.EntityModels;

namespace MoneyTracker2.Models.ViewModels;

public class TransactionView
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public float Amount { get; set; }
    public string Contact { get; set; }
    public string Reference { get; set; }

    public CategoryView Category { get; set; }
}