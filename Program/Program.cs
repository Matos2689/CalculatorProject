using CalculatorMethods.BusinessLogic;
using CalculatorMethods.Contracts;
using CalculatorMethods.Persistance;
using UnitsNet;

namespace Program {
    public class Program 
    {
        private static readonly string JsonPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SaveMathlog.json");

        private static readonly string XmlPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "SaveMathlog.xml");

        // Connection string to SQL
        private const string ConnStr =
            "Server=.;Database=SQL_Calculator_DB;Trusted_Connection=True;Encrypt=False;";

        static void Main(string[] args) {

            var calculator = new Calculator();

            var jsonRepo = new JsonRepositoryManager();
            var xmlRepo = new XmlRepositoryManager();
            var sqlRepo = new AdoNetRepositoryManager(ConnStr);

            var dataDir = Path.GetDirectoryName(JsonPath);
            if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);

            ConsoleExecution(calculator, jsonRepo, xmlRepo, sqlRepo);
        }
        private static void ConsoleExecution(
            Calculator calc, 
            JsonRepositoryManager jsonRepo,
            XmlRepositoryManager xmlRepo,
            AdoNetRepositoryManager sqlRepo
            )
        {
            double? lastNumeric = null;
            IQuantity? lastQuantity = null;

            while (true)
            {
                Console.Write("\n=> ");                

                var strInput = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(strInput) || strInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                // Special Commands...
                if (strInput.Equals("savejson", StringComparison.OrdinalIgnoreCase)) 
                { 
                    jsonRepo.Save(calc.MathLog, JsonPath); 
                    Console.WriteLine("History saved to SaveMathlog.json\n");
                    continue; 
                }

                if (strInput.Equals("savexml", StringComparison.OrdinalIgnoreCase))
                { 
                    xmlRepo.Save(calc.MathLog, XmlPath); 
                    Console.WriteLine("History saved to SaveMathlog.xml\n");
                    continue; }

                if (strInput.Equals("savesql", StringComparison.OrdinalIgnoreCase))
                { sqlRepo.Save(calc.MathLog, null!); 
                    Console.WriteLine("History saved to SQL Database\n");
                    continue; }

                if (strInput.Equals("loadjson", StringComparison.OrdinalIgnoreCase)) 
                { 
                    jsonRepo.Load(JsonPath);
                    calc.MathLog.Clear();
                    calc.MathLog.AddRange(jsonRepo.Load(JsonPath));
                    ShowCalculationHistory(calc);
                    Console.WriteLine("History loaded from SaveMathlog.json\n");
                    continue;
                }

                if(strInput.Equals("loadxml", StringComparison.OrdinalIgnoreCase)) 
                { 
                    calc.MathLog.Clear();
                    calc.MathLog.AddRange(xmlRepo.Load(XmlPath));
                    ShowCalculationHistory(calc);
                    Console.WriteLine("History loaded from SaveMathlog.xml\n");
                    continue;
                }

                if(strInput.Equals("loadsql", StringComparison.OrdinalIgnoreCase)) 
                { 
                    calc.MathLog.Clear();
                    calc.MathLog.AddRange(sqlRepo.Load(null!));
                    ShowCalculationHistory(calc);
                    Console.WriteLine("History loaded from SQL Database\n");
                    continue;
                }                
            
                if (strInput.Equals("clean", StringComparison.OrdinalIgnoreCase))
                { 
                    lastNumeric = null; 
                    continue; 
                }  

                try
                {
                    // Calculate and show
                    var log = calc.Calculate(strInput);

                    if (log.Type == MathLogTypes.NumericBased)
                    {
                        Console.WriteLine($"Result: {log.NumericResult}");
                    }
                    else 
                    {
                        Console.WriteLine($"Result: {log.QuantityResult}");
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
