using System.Globalization;
using System.Text.RegularExpressions;
using UnitsNet;
using UnitsNet.Units;

namespace CalculatorClasses {
    public class CalculatorMethods {

        public List<MathLogItem> MathLog { get; private set; } = new();

        // todo: don't return object, but a more specific type. 
        public MathLogItem Calculate(string input) {

            bool hasUnit = input.Any(char.IsLetter);

            var mathLog = new MathLogItem(input);
            if (hasUnit) {
                mathLog.SetQuantityResult(CalculateWithUnits(input));

            } else {
                mathLog.SetNumericResult(CalculateWithoutUnits(input));
            }

            MathLog.Add(mathLog);
            return mathLog;
        }

        private IQuantity CalculateWithUnits(string input) {

            // Only (km, m, cm, mm) pass
            FoundUnits(input);            

            var parts = SplitBasedOnOperands(input).ToArray();

            int idxOp = Array
                .FindIndex(parts, p => p == "+" || p == "-" || p == "*" || p == "/");
            
            if (idxOp == -1) {
                throw new IndexOutOfRangeException("Operator not found!");
            }

            var number1 = ParseLengthWithDefaultUnit(parts[idxOp - 1].Trim());
            var number2 = ParseLengthWithDefaultUnit(parts[idxOp + 1].Trim());

            char operand = parts[idxOp][0];

            IQuantity result = operand switch {
                '+' => number1 + number2,
                '-' => number1 - number2,
                '*' => number1 * number2,
                '/' => Ratio.FromDecimalFractions(number1 / number2),
            };
            return result;
        }

        private static bool FoundUnits(string input) {
            return Regex.IsMatch(input.Trim(), @"^\d+(\.\d+)?\s*(?:mm|m|cm|km)$"
            , RegexOptions.IgnoreCase);
        }

        private static Length ParseLengthWithDefaultUnit(string input) {

            bool hasUnit = input.Any(char.IsLetter);

            return hasUnit
                ? Length.Parse(input, CultureInfo.InvariantCulture)
                : Length.FromMeters(double.Parse(input, CultureInfo.InvariantCulture));
        }

        private static IEnumerable<string> SplitBasedOnOperands(string input) {

            var result = Regex
                .Split(input, @"(\+|\-|\*|/)")
                .Where(p => !string.IsNullOrWhiteSpace(p));
            return result;
        }

        private double CalculateWithoutUnits(string input) {
            //I receive any expression
            var parts = SplitBasedOnOperands(input).ToList();

            // If the string starts with a negative number
            if (parts[0] == "-") parts.Insert(0, "0");

            int index;

            // Multiplication and Division first
            while ((index = GetIndexOfMultiplyOrDivision(parts)) != -1) {

                var left = double.Parse(parts[index - 1]);
                var right = double.Parse(parts[index + 1]);

                var res = parts[index] == "*"
                    ? Multiply(new[] { left, right })
                    : Divide(new[] { left, right });

                parts[index - 1] = res.ToString();
                parts.RemoveRange(index, 2);
            }

            // Sum and Subtract last
            while ((index = GetIndexOfAdditionOrSubtraction(parts)) != -1) {

                var left = double.Parse(parts[index - 1]);
                var right = double.Parse(parts[index + 1]);

                var res = parts[index] == "+"
                    ? Add(new[] { left, right })
                    : Subtract(new[] { left, right });

                parts[index - 1] = res.ToString();
                parts.RemoveRange(index, 2);
            }

            // Rested Only one result
            var result = double.Parse(parts[0]);

            return result;
        }

        private static int GetIndexOfAdditionOrSubtraction(List<string> parts) {
            return parts.FindIndex(p => p == "+" || p == "-");
        }

        private static int GetIndexOfMultiplyOrDivision(List<string> parts) {
            return parts.FindIndex(p => p == "*" || p == "/");
        }

        public double Add(double[] input) => input.Aggregate((a, b) => a + b);
        public double Subtract(double[] input) => input.Aggregate((a, b) => a - b);
        public double Multiply(double[] input) => input.Aggregate((a, b) => a * b);
        public double Divide(double[] input) => input.Aggregate((a, b) => a / b);
    }
}
