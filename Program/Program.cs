using CalculatorProject.BusinessLogic;
using CalculatorProject.Contracts;
using CalculatorProject.Persistance;
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

            var sqlRepo = new SQLRepositoryManager(ConnStr);
            var _calculator = new Calculator(sqlRepo);      

            var jsonRepo = new JsonRepositoryManager();
            var xmlRepo = new XmlRepositoryManager();

            var dataDir = Path.GetDirectoryName(JsonPath);
            if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);

            ConsoleExecution(_calculator, jsonRepo, xmlRepo, sqlRepo);
        }
        private static void ConsoleExecution(
            Calculator calc, 
            JsonRepositoryManager jsonRepo,
            XmlRepositoryManager xmlRepo,
            SQLRepositoryManager sqlRepo
            )
        {            

            while (true)
            {
                Console.Write("\n=> ");                

                var strInput = Console.ReadLine().Trim();

                if (strInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (string.IsNullOrEmpty(strInput))
                {
                    Console.WriteLine("Please enter a valid expression or command.");
                    continue;
                }

                // Special Commands...
                if (strInput.Equals("savejson", StringComparison.OrdinalIgnoreCase)) 
                { 
                    jsonRepo.Save(calc.Memory, JsonPath); 
                    Console.WriteLine("History saved to SaveMathlog.json\n");
                    continue; 
                }

                if (strInput.Equals("savexml", StringComparison.OrdinalIgnoreCase))
                { 
                    xmlRepo.Save(calc.Memory, XmlPath); 
                    Console.WriteLine("History saved to SaveMathlog.xml\n");
                    continue; 
                }

                if (strInput.Equals("savesql", StringComparison.OrdinalIgnoreCase))
                { sqlRepo.Save(calc.Memory, null!); 
                    Console.WriteLine("History saved to SQL Database\n");
                    continue; 
                }

                if (strInput.Equals("loadjson", StringComparison.OrdinalIgnoreCase)) 
                { 
                    jsonRepo.Load(JsonPath);
                    calc.Memory.Clear();
                    calc.Memory.AddRange(jsonRepo.Load(JsonPath));
                    ShowCalculationHistory(calc);
                    Console.WriteLine("\nHistory loaded from SaveMathlog.json\n");
                    continue;
                }

                if(strInput.Equals("loadxml", StringComparison.OrdinalIgnoreCase)) 
                { 
                    calc.Memory.Clear();
                    calc.Memory.AddRange(xmlRepo.Load(XmlPath));
                    ShowCalculationHistory(calc);
                    Console.WriteLine("\nHistory loaded from SaveMathlog.xml\n");
                    continue;
                }

                if(strInput.Equals("loadsql", StringComparison.OrdinalIgnoreCase)) 
                { 
                    calc.Memory.Clear();
                    calc.Memory.AddRange(sqlRepo.Load(null!));
                    ShowCalculationHistory(calc);
                    Console.WriteLine("\nHistory loaded from SQL Database\n");
                    continue;
                }                
            
                if (strInput.Equals("clear", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    calc.Memory.Clear();
                    ShowCalculationHistory(calc);
                    Console.WriteLine("\nHistory cleared\n");
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
            foreach (var log in calc.Memory)
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
