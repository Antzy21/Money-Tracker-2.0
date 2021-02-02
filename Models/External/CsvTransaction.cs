namespace MoneyTracker.Models
{
    public class CsvTransaction
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Account { get; set; }
        public string Amount { get; set; }
        public string Subcategory { get; set; }
        public string Memo { get; set; }
    }
}
