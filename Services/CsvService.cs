﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MoneyTracker.Services
{
    public class CsvService
    {
        public CsvService()
        {
        }

        public List<T> ReadCsvTo<T>(IFormFile file) where T : new()
        {
            Stream stream = file.OpenReadStream();
            StreamReader streamReader = new StreamReader(stream);

            var listOfObjects = new List<T>();

            string line = streamReader.ReadLine();

            var headers = line.Split(',');
            var rows = new List<Dictionary<string, string>>();

            line = streamReader.ReadLine();
            while (line != null)
            {
                if (line.Trim().Length > 0)
                {
                    var row = new Dictionary<string, string>();
                    foreach (var cell in line.Split(',').Select((l, i) => new { element = l, idx = i }))
                    {
                        row.Add(headers[cell.idx], cell.element);
                    }
                    rows.Add(row);
                }
                line = streamReader.ReadLine();
            }

            foreach (var row in rows)
            {
                listOfObjects.Add(ConvertLineTo<T>(row));
            }

            return listOfObjects;
        }

        private static T ConvertLineTo<T>(Dictionary<string, string> row) where T : new()
        {
            T obj = new T();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var entry = row[property.Name];
                property.SetValue(obj, entry);
            }

            return obj;
        }
    }
}
