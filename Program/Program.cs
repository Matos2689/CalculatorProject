using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CalculatorClasses;
using System.IO;
using UnitsNet;
using System.Security.Cryptography.X509Certificates;

namespace Program {
    public class Program {

        static void Main(string[] args) {

            var calc = new CalculatorMethods();
            var JsonClass = new JsonHistoryManager();

            static void Local() {
                var path = Path.GetFullPath("SaveMathlog.json");
                Console.WriteLine($"File saved in: {path}");
            }

            Local();


            ConsoleExecution(calc, JsonClass);
        }

        private static void ConsoleExecution(CalculatorMethods calc, JsonHistoryManager JsonClass) {
            while (true) {

                Console.Write("=> ");

                try {

                    string? input = Console.ReadLine();

                    if (input == null) break;

                    if (input.Length == 0) continue;

                    if (string.Equals(input, "save", StringComparison.OrdinalIgnoreCase)) {
                        SaveJSONFile(calc, JsonClass);
                        continue;
                    }

                    if (string.Equals(input, "load", StringComparison.OrdinalIgnoreCase)) {
                        LoadJSONFile(JsonClass);
                        continue;
                    }

                    if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase)) break;

                    EvaluateAndDisplayResult(input, calc);

                    ShowCalculationHistory(calc);
                } catch (FormatException fx) {
                    Console.WriteLine($"Invalid Format: {fx.Message}");
                } catch (Exception ex) {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void EvaluateAndDisplayResult(string input, CalculatorMethods calc) {
            try {
                var result = calc.Calculate(input);

                Console.WriteLine($"\nResult: {result}");

            } catch (Exception ex) {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }

        private static void SaveJSONFile(CalculatorMethods calc, JsonHistoryManager JsonClass) {
            JsonClass.SaveHistoryJson(calc.MathLog);
            Console.WriteLine("History saved in SaveMathlog.json\n");
        }

        private static void LoadJSONFile(JsonHistoryManager JsonClass) {
            JsonClass.LoadHistoryJson();
            Console.WriteLine("File Read Successfully!\n");
        }

        private static void ShowCalculationHistory(CalculatorMethods calc) {
            Console.WriteLine("\noperations:");
            foreach (var log in calc.MathLog) {

                switch (log.Type) {

                    case MathLogTypes.NumericBased:
                        Console.WriteLine($"{log.Expression} = {log.NumericResult}");
                        break;

                    case MathLogTypes.UnitBased:
                        Console.WriteLine($"{log.Expression} = {log.QuantityResult}");
                        break;
                }
            }
        }
    }
}
