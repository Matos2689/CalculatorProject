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
        private readonly string FilePath;
        public JsonRepositoryManager(string filePath = "SaveMathlog.json")
        {
            FilePath = filePath;
        }
        public void Save(List<MathLogItem> logs)
        {
            // Convert MathLogItem to MathLogEntity
            var save = logs.ToEntities();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var json = JsonSerializer.Serialize(save, options);
            File.WriteAllText(FilePath, json);
        }

        public List<MathLogItem> Load()
        {
            if (!File.Exists(FilePath))
                return new List<MathLogItem>();

            var json = File.ReadAllText(FilePath);

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

        public void Read()
        {
            Console.WriteLine(File.Exists(FilePath)
                ? File.ReadAllText(FilePath)
                : "There is no file named SaveMathlog.json");
        }
    }
}
