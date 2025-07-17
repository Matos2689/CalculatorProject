using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorProject.BusinessLogic
{
    public class NumericParser
    {
        public double CalculateWithoutUnits(string input)
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

        public static IEnumerable<string> SplitBasedOnOperands(string input)
        {
            var result = Regex.Split(input, @"(\+|\-|\*|/)")
                .Where(p => !string.IsNullOrWhiteSpace(p));

            return result;
        }

        private double Add(double[] input) => input.Aggregate((a, b) => a + b);

        private double Subtract(double[] input) => input.Aggregate((a, b) => a - b);

        private double Multiply(double[] input) => input.Aggregate((a, b) => a * b);

        private double Divide(double[] input) => input.Aggregate((a, b) => a / b);
        
    }
}
