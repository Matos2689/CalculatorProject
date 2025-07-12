using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorMethods.BusinessLogic
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
