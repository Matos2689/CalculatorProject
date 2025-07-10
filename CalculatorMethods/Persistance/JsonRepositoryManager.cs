using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using CalculatorMethods.Contracts;
using UnitsNet;

namespace CalculatorMethods.Persistance
{
    public class JsonRepositoryManager : IRepository
    {
        public void Save(List<MathLogItem> logs, string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Convert MathLogItem to MathLogEntity
            var entities = logs.ToEntities();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var json = JsonSerializer.Serialize(entities, options);
            File.WriteAllText(filePath, json);
        }

        public List<MathLogItem> Load(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<MathLogItem>();

            var json = File.ReadAllText(filePath);

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var entities = JsonSerializer.Deserialize<List<MathLogEntity>>(json);

            if (entities == null)
            {
                return new List<MathLogItem>();
            }

            // Convert MathLogEntity to MathLogItem
            var result = entities.Select(e => e.FromEntity()).ToList();

            return result;
        }

        public void Read(string filePath)
        {
            Console.WriteLine(File.Exists(filePath)
                ? File.ReadAllText(filePath)
                : "There is no file named SaveMathlog.json");
        }
    }
}
