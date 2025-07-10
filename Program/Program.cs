using CalculatorMethods.BusinessLogic;
using CalculatorMethods.Contracts;
using CalculatorMethods.Persistance;

namespace Program {
    public class Program {

        static void Main(string[] args) {

            //var calculator = new Calculator();
            //var jsonRepoManager = new JsonRepositoryManager();
           
            //ConsoleExecution(calculator, jsonRepoManager);
        }

        //private static void ConsoleExecution(Calculator calc, JsonRepositoryManager JsonRepo) {
        //    while (true) {

        //        Console.Write("=> ");

        //        try {

        //            string? input = Console.ReadLine();

        //            if (input == null || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
        //                break;

        //            if (input.Length == 0) continue;

        //            if (string.Equals(input, "save", StringComparison.OrdinalIgnoreCase)) 
        //            {
        //                SaveJSONFile(calc, JsonRepo);
        //                continue;
        //            }

        //            if (string.Equals(input, "read", StringComparison.OrdinalIgnoreCase)) 
        //            {
        //                ReadJSONFile(JsonRepo);
        //                continue;
        //            }

        //            if (string.Equals(input, "load", StringComparison.OrdinalIgnoreCase))
        //            {
        //                LoadJSONFile(calc, JsonRepo);
        //                continue;
        //            }

        //            EvaluateAndDisplayResult(input, calc);

        //            ShowCalculationHistory(calc);
        //        } 
        //        catch (FormatException fx) 
        //        {

        //            Console.WriteLine($"Invalid Format: {fx.Message}");
        //        } 
        //        catch (Exception ex) 
        //        {

        //            Console.WriteLine($"Error: {ex.Message}");
        //        }
        //    }
        //}

        //private static void EvaluateAndDisplayResult(string input, Calculator calc) {
        //    try {
        //        var log = calc.Calculate(input);

        //        if (log.Type == MathLogTypes.NumericBased) {
        //            Console.WriteLine($"Result: {log.NumericResult}");
        //        } else if (log.Type == MathLogTypes.UnitBased) {
        //            Console.WriteLine($"Result: {log.QuantityResult}");
        //        }

        //    } catch (Exception ex) {
        //        Console.WriteLine($"Unexpected Error: {ex.Message}");
        //    }
        //}

        //private static void SaveJSONFile(Calculator calc, JsonRepositoryManager JsonClass) {
        //    JsonClass.Save(calc.MathLog, filePath);
        //    Console.WriteLine("History saved in SaveMathlog.json\n");
        //}

        //private static void LoadJSONFile(Calculator calc, JsonRepositoryManager jsonRepo)
        //{
        //    var loadItems = jsonRepo.Load();
        //    calc.MathLog.Clear();
        //    calc.MathLog.AddRange(loadItems);
        //    Console.WriteLine("History loaded from SaveMathlog.json\n");
        //}

        //private static void ReadJSONFile(JsonRepositoryManager JsonClass) {
        //    JsonClass.Read();
        //    Console.WriteLine("File Read Successfully!\n");
        //}

        //private static void ShowCalculationHistory(Calculator calc) {
        //    Console.WriteLine("\noperations:");
        //    foreach (var log in calc.MathLog) {

        //        switch (log.Type) {

        //            case MathLogTypes.NumericBased:
        //                Console.WriteLine($"{log.Expression} = {log.NumericResult}");
        //                break;

        //            case MathLogTypes.UnitBased:
        //                Console.WriteLine($"{log.Expression} = {log.QuantityResult}");
        //                break;
        //        }
        //    }
        //}
    }
}
