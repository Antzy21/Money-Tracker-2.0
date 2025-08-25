using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using MoneyTracker2.Utility;

namespace MoneyTracker2.Services;

public class CsvService
{
    public CsvService()
    {
    }

    public List<Result<T>> ReadCsvTo<T>(IFormFile file, bool mergeRemainingLinesIntoLast = true) where T : new()
    {
        Stream stream = file.OpenReadStream();
        StreamReader streamReader = new StreamReader(stream);

        var listOfObjects = new List<Result<T>>();

        string line = streamReader.ReadLine()!;

        var headers = line.Split(',');
        var rows = new List<Dictionary<string, string>>();

        line = streamReader.ReadLine()!;
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
                        cells = line.Split(',', headers.Length);
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
            line = streamReader.ReadLine()!;
        }

        foreach (var row in rows)
        {
            listOfObjects.Add(ConvertLineTo<T>(row));
        }

        return listOfObjects;
    }

    private static Result<T> ConvertLineTo<T>(Dictionary<string, string> row) where T : new()
    {
        T obj = new T();

        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            string value = row[property.Name];
            try
            {
                AssignProperty(obj, property, value);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure($"Error assigning property {property.Name} with value {value}: {ex.Message}");
            }
        }

        return Result<T>.Success(obj);
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