using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using UnitsNet;

namespace CalculatorClasses {
    public class JsonHistoryManager {

        public void SaveHistoryJson(List<MathLogItem> logs) {

            var toSave = logs.Select(log => new {

                log.Expression,
                Type = log.Type.ToString(),
                Result = log.Type == MathLogTypes.NumericBased 
                ? (object)log.NumericResult
                : log.QuantityResult.ToString()})
                .ToList();

            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web
                .JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var json = JsonSerializer.Serialize(toSave, options);
            File.WriteAllText("SaveMathlog.json", json);
        }

        public void LoadHistoryJson() {
            if (File.Exists("SaveMathlog.json")) {
                var json = File.ReadAllText("SaveMathlog.json");
                Console.WriteLine(json);
            }
        }
    }
}
