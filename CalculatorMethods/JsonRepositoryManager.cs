using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using UnitsNet;

namespace CalculatorMethods {
    public class JsonRepositoryManager {

        public void Save(List<MathLogItem> logs) {

            // Convert MathLogItem to MathLogEntity
            var save = logs.ToEntities();

            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var json = JsonSerializer.Serialize(save, options);
            File.WriteAllText("SaveMathlog.json", json);
        }

        public void Read() {
            if (File.Exists("SaveMathlog.json")) {
                var json = File.ReadAllText("SaveMathlog.json");
                Console.WriteLine(json);
            }
        }
    }
}
