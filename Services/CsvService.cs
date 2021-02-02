using CsvHelper;
using MoneyTracker.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MoneyTracker.Services
{
    public class CsvService
    {
        public CsvService()
        {
        }

        public List<CsvTransaction> ReadCsv(string path)
        {
            var reader = new StreamReader(path);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<CsvTransaction>().ToList();

            return records;
        }
    }
}
