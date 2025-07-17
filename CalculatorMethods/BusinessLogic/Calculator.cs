using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using CalculatorProject.Contracts;
using UnitsNet;

namespace CalculatorProject.BusinessLogic
{
    public class Calculator
    {
        private IRepository _repository;
        public List<MathLogItem> Memory { get; set; } = [];
        
        public Calculator(IRepository repository)
        {
            _repository = repository;
        }

        public MathLogItem Calculate(string input)
        {
            var mathLog = new MathLogItem(input);

            (input.Any(char.IsLetter)
                ? new Action(() => mathLog.SetQuantityResult(CalculateWithUnits(input)))
                : new Action(() => mathLog.SetNumericResult(CalculateWithoutUnits(input)))
            )();

            Memory.Add(mathLog);
            return mathLog;
        }

        // If the input is numeric based, it will calculate the result without units.
        private double CalculateWithoutUnits(string input)
        {
            //I receive any expression
            var parts = SplitBasedOnOperands(input).ToList();

            // If the string starts with a negative number
            if (parts[0] == "-") parts.Insert(0, "0");

            int index;

            // Verify if there are percentages before any calculations
            NormalizePercentages(parts);

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

        private static void NormalizePercentages(List<string> parts)
        {
            // pattern to match "50%40" or "50% 40" without explicit operators
            var noOperator = new Regex(@"^(\d+(?:\.\d+)?)%\s*(\d+(?:\.\d+)?)$");

            for (int i = 0; i < parts.Count; i++)
            {
                var token = parts[i].Trim();

                var m = noOperator.Match(token);
                if (m.Success)
                {
                    var percentageValue = double.Parse(m.Groups[1].Value) / 100;
                    var nextNumber = m.Groups[2].Value;

                    parts[i] = percentageValue.ToString();
                    parts.Insert(i + 1, "*");
                    parts.Insert(i + 2, nextNumber);

                    i += 2;  // skip the newly inserted tokens
                    continue;
                }

                // Check if the token ends with a percentage sign
                if (token.EndsWith("%") && double.TryParse(token[..^1], out var value))
                {
                    // modify the token to represent a decimal value
                    parts[i] = (value / 100).ToString();
                }
            }
        }

        private static int GetIndexOfAdditionOrSubtraction(List<string> parts)
        {
            return parts.FindIndex(p => p == "+" || p == "-");
        }

        private static int GetIndexOfMultiplyOrDivision(List<string> parts)
        {
            return parts.FindIndex(p => p == "*" || p == "/");
        }

        private static IEnumerable<string> SplitBasedOnOperands(string input)
        {
            var result = Regex.Split(input, @"(\+|\-|\*|/)")
                .Where(p => !string.IsNullOrWhiteSpace(p));

            return result;
        }

        private double Add(double[] input) => input.Aggregate((a, b) => a + b);

        private double Subtract(double[] input) => input.Aggregate((a, b) => a - b);

        private double Multiply(double[] input) => input.Aggregate((a, b) => a * b);

        private double Divide(double[] input) => input.Aggregate((a, b) => a / b);

        // If CalculateWithUnits is called, it will parse the input string
        private IQuantity CalculateWithUnits(string input)
        {
            var parts = SplitBasedOnOperands(input)
                              .Select(t => t.Replace(" ", ""))
                              .Where(t => t.Length > 0)
                              .ToList();

            // List containing IQuantity (values) and string (operators)
            List<object> ListOfObjects = ParseQuantityWithDefaultUnit(parts);

            int index;

            // 1) Multiplication / Division
            while ((index = GetIndexOfMultOrDivForUnits(ListOfObjects)) != -1)
            {
                var op = (string)ListOfObjects[index];
                dynamic left = ListOfObjects[index - 1];
                dynamic right = ListOfObjects[index + 1];
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

                ListOfObjects[index - 1] = res;
                ListOfObjects.RemoveRange(index, 2);
            }

            // 2) Addition / Subtraction
            while ((index = GetIndexOfAddOrSubForUnits(ListOfObjects)) != -1)
            {
                var op = (string)ListOfObjects[index];
                dynamic left = (IQuantity)ListOfObjects[index - 1];
                dynamic right = (IQuantity)ListOfObjects[index + 1];

                IQuantity res = op == "+"
                    ? left + right
                    : left - right;

                ListOfObjects[index - 1] = res;
                ListOfObjects.RemoveRange(index, 2);
            }

            return (IQuantity)ListOfObjects[0];
        }

        private static int GetIndexOfAddOrSubForUnits(List<object> itemsList)
        {
            return itemsList.FindIndex(x => x is string op && (op == "+" || op == "-"));
        }

        private static int GetIndexOfMultOrDivForUnits(List<object> items)
        {
            return items.FindIndex(x => x is string op && (op == "*" || op == "/"));
        }

        private static List<object> ParseQuantityWithDefaultUnit(List<string> input)
        {
            var lengthUnits = new HashSet<string> { "mm", "cm", "m", "km", "dm", "hm", "dam" };
            var volumeUnits = new HashSet<string> { "ml", "cl", "l", "kl", "dl", "dal", "hl" };
            var massUnits = new HashSet<string> { "mg", "cg", "dg", "g", "dag", "hg", "kg" };
            var operators = new HashSet<string> { "+", "-", "*", "/" };

            var results = new List<object>();

            string defaultUnit = "m";
            if (input.Any(t => volumeUnits.Contains(GetUnit(t)))) defaultUnit = "l";
            else if (input.Any(t => massUnits.Contains(GetUnit(t)))) defaultUnit = "g";

            foreach (var element in input)
            {
                var txt = element.Replace(" ", "").ToLowerInvariant();

                if (operators.Contains(txt))
                {
                    results.Add(txt);
                    continue;
                }

                var numberPart = new string(txt.TakeWhile(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
                var unitPart = new string(txt.SkipWhile(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());

                // if empty unitPart, use default unit
                if (string.IsNullOrEmpty(unitPart))
                    unitPart = defaultUnit;

                var txtForParser = numberPart + unitPart;
                txtForParser = txtForParser.Replace(",", ".");

                // calls the correct parser
                object parsed = unitPart switch
                {
                    _ when lengthUnits.Contains(unitPart) => ParseLengthWithDefaultUnit(txtForParser),
                    _ when volumeUnits.Contains(unitPart) => ParseVolumeWithDefaultUnit(txtForParser),
                    _ when massUnits.Contains(unitPart) => ParseMassWithDefaultUnit(txtForParser),
                    _ => throw new InvalidOperationException(
                             $"Unit not supported: '{unitPart}' in '{element}'")
                };

                results.Add(parsed);
            }

            return results;
        }

        private static string GetUnit(string token)
        {
            var txt = token.Replace(" ", "").ToLowerInvariant();
            return new string(txt.SkipWhile(c => char.IsDigit(c) || c == '.').ToArray());
        }

        private static Length ParseLengthWithDefaultUnit(string input)
        {

            return Length.Parse(input, CultureInfo.InvariantCulture);
        }

        private static Volume ParseVolumeWithDefaultUnit(string input)
        {
            return Volume.Parse(input, CultureInfo.InvariantCulture);
        }

        private static Mass ParseMassWithDefaultUnit(string input)
        {
            return Mass.Parse(input, CultureInfo.InvariantCulture);
        }
    }
}
