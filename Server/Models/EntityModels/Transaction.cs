using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker2.Models.EntityModels;

public class Transaction
{
    [Key]
    public int Id { get; init; }
    public DateTime Date { get; set; }
    public float Amount { get; set; }
    public required string Contact { get; set; }
    public required string Reference { get; set; }
}