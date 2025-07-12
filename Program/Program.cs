using CalculatorMethods.BusinessLogic;
using CalculatorMethods.Contracts;
using CalculatorMethods.Persistance;
using UnitsNet;

namespace Program {
    public class Program 
    {
        // Você pode jogar esse filePath pra dentro da pasta Data, por exemplo
        private static readonly string filePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SaveMathlog.json");

        static void Main(string[] args) {

            var calculator = new Calculator();
            var jsonRepoManager = new JsonRepositoryManager();

            var dataDir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);

            ConsoleExecution(calculator, jsonRepoManager);
        }
        private static void ConsoleExecution(Calculator calc, JsonRepositoryManager JsonRepo)
        {
            double? lastNumeric = null;
            IQuantity? lastQuantity = null;

            while (true)
            {
                Console.Write("=> ");
                if (lastNumeric.HasValue)
                    Console.Write($"{lastNumeric} ");

                var strInput = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(strInput) || strInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                // Special Commands...
                if (strInput.Equals("save", StringComparison.OrdinalIgnoreCase)) { SaveJSONFile(calc, JsonRepo); continue; }
                if (strInput.Equals("read", StringComparison.OrdinalIgnoreCase)) { ReadJSONFile(JsonRepo); continue; }
                if (strInput.Equals("load", StringComparison.OrdinalIgnoreCase)) { LoadJSONFile(calc, JsonRepo); continue; }

                // 2) If only digited a operator
                string input = strInput;
                if (lastNumeric.HasValue && strInput.Length > 0 && "+-*/".Contains(strInput[0]))
                    input = $"{lastNumeric}{strInput}";

                try
                {
                    // Calculate and show
                    var log = calc.Calculate(input);
                    if (log.Type == MathLogTypes.NumericBased)
                    {
                        Console.WriteLine($"Result: {log.NumericResult}");
                        lastNumeric = log.NumericResult;           // Update numeric
                    }
                    else // UnitBased
                    {
                        Console.WriteLine($"Result: {log.QuantityResult}");
                        lastQuantity = log.QuantityResult;         // Update quantity
                        lastNumeric = null;
                    }

                    ShowCalculationHistory(calc);
                }
                catch (FormatException fx)
                {
                    Console.WriteLine($"Invalid Format: {fx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }        

        private static void SaveJSONFile(Calculator calc, JsonRepositoryManager JsonClass)
        {
            JsonClass.Save(calc.MathLog, filePath);
            Console.WriteLine("History saved in SaveMathlog.json\n");
        }

        private static void LoadJSONFile(Calculator calc, JsonRepositoryManager jsonRepo)
        {
            var loadItems = jsonRepo.Load( filePath);
            calc.MathLog.Clear();
            calc.MathLog.AddRange(loadItems);
            Console.WriteLine("History loaded from SaveMathlog.json\n");
        }

        private static void ReadJSONFile(JsonRepositoryManager JsonClass)
        {
            JsonClass.Read( filePath);
            Console.WriteLine("File Read Successfully!\n");
        }

        private static void ShowCalculationHistory(Calculator calc)
        {
            Console.WriteLine("\noperations:");
            foreach (var log in calc.MathLog)
            {

                switch (log.Type)
                {

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
