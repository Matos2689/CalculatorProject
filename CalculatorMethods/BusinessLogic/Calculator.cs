using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using CalculatorMethods.Contracts;
using UnitsNet;

namespace CalculatorMethods.BusinessLogic
{
    public class Calculator
    {

        public List<MathLogItem> MathLog { get; private set; } = new();

        // todo: don't return object, but a more specific type. 
        public MathLogItem Calculate(string input)
        {

            bool hasUnit = input.Any(char.IsLetter);

            var mathLog = new MathLogItem(input);

            if (hasUnit)
            {
                mathLog.SetQuantityResult(CalculateWithUnits(input));
            }
            else
            {
                mathLog.SetNumericResult(CalculateWithoutUnits(input));
            }

            MathLog.Add(mathLog);

            return mathLog;
        }

        public IQuantity CalculateWithUnits(string input)
        {
            // Only (km,m,cm,mm) pass
            FoundUnits(input);

            var parts = SplitBasedOnOperands(input)
                              .Select(t => t.Trim())
                              .Where(t => t.Length > 0)
                              .ToList();

            var operators = new HashSet<string> { "+", "-", "*", "/" };

            ValidateOperators(parts, operators);

            // List containing IQuantity (values) and string (ops)
            List<object> itemsList = BuildMixedList(parts);

            int index;

            // 1) Multiplication / Division
            while ((index = GetIndexOfMultOrDivForUnits(itemsList)) != -1)
            {
                var op = (string)itemsList[index];
                dynamic left = itemsList[index - 1];
                dynamic right = itemsList[index + 1];
                IQuantity res;

                if (op == "*")
                {
                    // Call Length*Length → Area, or Volume*Length→ etc.
                    res = (IQuantity)(left * right);
                }
                else
                {
                    double ratio = (double)(left / right);
                    res = Ratio.FromDecimalFractions(ratio);
                }

                itemsList[index - 1] = res;
                itemsList.RemoveRange(index, 2);
            }

            // 2) Addition / Subtraction
            while ((index = GetIndexOfAddOrSubForUnits(itemsList)) != -1)
            {
                var op = (string)itemsList[index];
                dynamic left = (IQuantity)itemsList[index - 1];
                dynamic right = (IQuantity)itemsList[index + 1];

                IQuantity res = op == "+"
                    ? left + right
                    : left - right;

                itemsList[index - 1] = res;
                itemsList.RemoveRange(index, 2);
            }

            return (IQuantity)itemsList[0];
        }

        private static int GetIndexOfAddOrSubForUnits(List<object> itemsList)
        {
            return itemsList.FindIndex(x => x is string op && (op == "+" || op == "-"));
        }

        private static int GetIndexOfMultOrDivForUnits(List<object> items)
        {
            return items.FindIndex(x => x is string op && (op == "*" || op == "/"));
        }

        private static List<object> BuildMixedList(List<string> parts)
        {
            return parts.Select(t => (object)(t is "+" or "-" or "*" or "/"
                        ? t
                        : ParseLengthWithDefaultUnit(t)))
                        .ToList();
        }

        private void ValidateOperators(List<string> rawTokens, HashSet<string> ops)
        {
            var invalid = rawTokens
                            .FirstOrDefault(t => !ops.Contains(t) && !TryParseQuantity(t));

            if (invalid != null)
                throw new InvalidOperationException(
                    $"Operator not found!: '{invalid}'");
        }

        private static bool FoundUnits(string input)
        {
            return Regex.IsMatch(input.Trim(), @"^\d+(\.\d+)?\s*(?:mm|m|cm|km)$"
            , RegexOptions.IgnoreCase);
        }

        private static Length ParseLengthWithDefaultUnit(string input)
        {

            bool hasUnit = input.Any(char.IsLetter);

            return hasUnit
                ? Length.Parse(input, CultureInfo.InvariantCulture)
                : Length.FromMeters(double.Parse(input, CultureInfo.InvariantCulture));
        }

        private static IEnumerable<string> SplitBasedOnOperands(string input)
        {

            var result = Regex
                .Split(input, @"(\+|\-|\*|/)")
                .Where(p => !string.IsNullOrWhiteSpace(p));
            return result;
        }

        private double CalculateWithoutUnits(string input)
        {
            //I receive any expression
            var parts = SplitBasedOnOperands(input).ToList();

            // If the string starts with a negative number
            if (parts[0] == "-") parts.Insert(0, "0");

            int index;

            // Multiplication and Division first
            while ((index = GetIndexOfMultiplyOrDivision(parts)) != -1)
            {

                var left = double.Parse(parts[index - 1]);
                var right = double.Parse(parts[index + 1]);

                var res = parts[index] == "*"
                    ? Multiply(new[] { left, right })
                    : Divide(new[] { left, right });

                parts[index - 1] = res.ToString();
                parts.RemoveRange(index, 2);
            }

            // Sum and Subtract last
            while ((index = GetIndexOfAdditionOrSubtraction(parts)) != -1)
            {

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

        private static int GetIndexOfAdditionOrSubtraction(List<string> parts)
        {
            return parts.FindIndex(p => p == "+" || p == "-");
        }

        private static int GetIndexOfMultiplyOrDivision(List<string> parts)
        {
            return parts.FindIndex(p => p == "*" || p == "/");
        }

        private bool TryParseQuantity(string token)
        {
            try
            {
                ParseLengthWithDefaultUnit(token);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public double Add(double[] input) => input.Aggregate((a, b) => a + b);
        public double Subtract(double[] input) => input.Aggregate((a, b) => a - b);
        public double Multiply(double[] input) => input.Aggregate((a, b) => a * b);
        public double Divide(double[] input) => input.Aggregate((a, b) => a / b);
    }
}
