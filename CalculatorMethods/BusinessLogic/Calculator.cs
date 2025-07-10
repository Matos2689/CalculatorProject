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

        private IQuantity CalculateWithUnits(string input)
        {
            var parts = SplitBasedOnOperands(input)
                              .Select(t => t.Trim())
                              .Where(t => t.Length > 0)
                              .ToList();

            var operators = new HashSet<string> { "+", "-", "*", "/" };

            var firstInvalidPart = GetFirstInvalidPart(parts, operators);

            if (firstInvalidPart != null)
                throw new InvalidOperationException(
                    $"Invalid operator or unit:'{firstInvalidPart}'");

            // List containing IQuantity (values) and string (operators)
            List<object> itemsList = BuildMixedList(parts, operators);

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

        private string? GetFirstInvalidPart(List<string> parts, HashSet<string> operators)
        {
            return parts.FirstOrDefault(t => !operators.Contains(t) && !TryParseQuantity(t));
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

        private static int GetIndexOfAddOrSubForUnits(List<object> itemsList)
        {
            return itemsList.FindIndex(x => x is string op && (op == "+" || op == "-"));
        }

        private static int GetIndexOfMultOrDivForUnits(List<object> items)
        {
            return items.FindIndex(x => x is string op && (op == "*" || op == "/"));
        }

        private static List<object> BuildMixedList(List<string> parts, HashSet<string> operators)
        {
            return parts
                .Select(t => operators.Contains(t)? (object) t
                : ParseQuantityWithDefaultUnit(t))
                .ToList();
        }

        private static IQuantity ParseQuantityWithDefaultUnit(string input)
        {
            var txt = input.Replace(" ", "");

            // isolates only the “unit”
            var unit = new string(txt.SkipWhile(c => char.IsDigit(c) || c == '.').ToArray())
                                .ToLowerInvariant();

            var lengthUnits = new HashSet<string> { "mm","cm","m","km","dm","hm","dam" };
            var volumeUnits = new HashSet<string> { "ml","cl","l","kl","dl","dal","hl" };
            var MassUnits = new HashSet<string> { "mg","cg","dg","g","dag","hg","kg" };

            if (unit == "" || lengthUnits.Contains(unit))
            {
                // Empty unit assumes meter
                return ParseLengthWithDefaultUnit(txt);
            }
            else if (volumeUnits.Contains(unit))
            {
                return ParseVolumeWithDefaultUnit(txt);
            }
            else if (MassUnits.Contains(unit))
            {
                return ParseMassWithDefaultUnit(txt);
            }
            else
            {
                throw new InvalidOperationException($"Unit not supported: '{unit}' in '{input}'");
            }
        }

        private static Length ParseLengthWithDefaultUnit(string input)
        {
            bool hasUnit = input.Any(char.IsLetter);
            
            return hasUnit
                ? Length.Parse(input, CultureInfo.InvariantCulture)
                : Length.FromMeters(double.Parse(input, CultureInfo.InvariantCulture));
        }

        private static Volume ParseVolumeWithDefaultUnit(string input)
        {
            return Volume.Parse(input, CultureInfo.InvariantCulture);                
        }

        private static Mass ParseMassWithDefaultUnit(string input)
        {
            return Mass.Parse(input, CultureInfo.InvariantCulture);                
        }

        private static IEnumerable<string> SplitBasedOnOperands(string input)
        {
            var result = Regex.Split(input, @"(\+|\-|\*|/)")
                .Where(p => !string.IsNullOrWhiteSpace(p));

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

        private static bool TryParseQuantity(string input)
        {
            input = input.Replace(" ", "");

            var units = new string
                (input.SkipWhile(c => char.IsDigit(c) || c == '.').ToArray())
                .ToLowerInvariant();
            try
            {
                ParseQuantityWithDefaultUnit(input);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private double Add(double[] input) => input.Aggregate((a, b) => a + b);

        private double Subtract(double[] input) => input.Aggregate((a, b) => a - b);

        private double Multiply(double[] input) => input.Aggregate((a, b) => a * b);

        private double Divide(double[] input) => input.Aggregate((a, b) => a / b);
    }
}
