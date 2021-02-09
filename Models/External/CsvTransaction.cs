﻿using System;

namespace MoneyTracker.Models
{
    public class CsvTransaction
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Account { get; set; }
        public float Amount { get; set; }
        public string Subcategory { get; set; }
        public string Memo { get; set; }
    }
}
