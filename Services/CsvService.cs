using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MoneyTracker.Services
{
    public class CsvService
    {
        public CsvService()
        {
        }

        public List<T> ReadCsvTo<T>(IFormFile file, LongLinePolicy longLinePolicy = LongLinePolicy.ThrowError) where T : new()
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
                    try
                    {
                        var row = new Dictionary<string, string>();
                        var cells = line.Split(',');
                        if (cells.Length != headers.Length)
                        {
                            switch (longLinePolicy)
                            {
                                case LongLinePolicy.Ignore:
                                    cells = cells.Take(6).ToArray();
                                    break;
                                case LongLinePolicy.IncludeInLastLine:
                                    cells = line.Split(',', 6);
                                    break;
                                case LongLinePolicy.ThrowError:
                                    Console.WriteLine($"Line too long: {line}");
                                    throw new ArgumentException($"Line too long: {line}");
                            }
                        }
                        foreach (var cell in cells.Select((l, i) => new { element = l, idx = i }))
                        {
                            row.Add(headers[cell.idx], cell.element);
                        }
                        rows.Add(row);
                    }
                    catch
                    {
                        Console.WriteLine($"Error adding line: {line}");
                    }
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
                string value = row[property.Name];
                AssignProperty(obj, property, value);
            }

            return obj;
        }

        private static void AssignProperty<T>(T obj, PropertyInfo property, string value) where T : new()
        {
            if (property.PropertyType == typeof(float))
            {
                float entry = float.Parse(value);
                property.SetValue(obj, entry);
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                DateTime entry = DateTime.Parse(value);
                property.SetValue(obj, entry);
            }
            else
            {
                var entry = value;
                property.SetValue(obj, entry);
            }
        }
    }

    public enum LongLinePolicy
    {
        Ignore,
        IncludeInLastLine,
        ThrowError
    }
}
