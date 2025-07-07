using CalculatorMethods;

namespace Program {
    public class Program {

        static void Main(string[] args) {

            var calc = new Calculator();
            var JsonClass = new JsonHistoryManager();

            ConsoleExecution(calc, JsonClass);
        }

        private static void ConsoleExecution(Calculator calc, JsonHistoryManager JsonClass) {
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

        private static void EvaluateAndDisplayResult(string input, Calculator calc) {
            try {
                var log = calc.Calculate(input);

                if (log.Type == MathLogTypes.NumericBased) {
                    Console.WriteLine($"Result: {log.NumericResult}");
                } else if (log.Type == MathLogTypes.UnitBased) {
                    Console.WriteLine($"Result: {log.QuantityResult}");
                }

            } catch (Exception ex) {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }

        private static void SaveJSONFile(Calculator calc, JsonHistoryManager JsonClass) {
            JsonClass.Save(calc.MathLog);
            Console.WriteLine("History saved in SaveMathlog.json\n");
        }

        private static void LoadJSONFile(JsonHistoryManager JsonClass) {
            JsonClass.Read();
            Console.WriteLine("File Read Successfully!\n");
        }

        private static void ShowCalculationHistory(Calculator calc) {
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
