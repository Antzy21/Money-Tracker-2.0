using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker2.Models.EntityModels;

public class Transaction
{
    [Key]
    public int Id { get; init; }
    public DateTime Date { get; set; }
    public float Amount { get; set; }
    public string Contact { get; set; }
    public string Reference { get; set; }

    public int? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}