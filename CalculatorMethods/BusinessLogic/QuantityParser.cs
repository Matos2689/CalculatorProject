using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorMethods.Contracts;
using UnitsNet;
using static System.Net.Mime.MediaTypeNames;

namespace CalculatorMethods.BusinessLogic
{
    public class QuantityParser
    {
        public IQuantity CalculateWithUnits(string input)
        {
            var parts = NumericParser.SplitBasedOnOperands(input) 
                              .Select(t => t.Replace(" ",""))
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

                var numberPart = new string(txt.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());
                var unitPart = new string(txt.SkipWhile(c => char.IsDigit(c) || c == '.').ToArray());

                // if empty unitPart, use default unit
                if (string.IsNullOrEmpty(unitPart))
                    unitPart = defaultUnit;

                var txtForParser = numberPart + unitPart;

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

    

