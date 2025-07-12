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

        public List<MathLogItem> MathLog { get; private set; } = [];
        public QuantityParser QuantityParser = new();
        public NumericParser NumericParser = new();

        // todo: don't return object, but a more specific type. 
        public MathLogItem Calculate(string input)
        {
            var mathLog = new MathLogItem(input);

            (input.Any(char.IsLetter)
                ? new Action(() => mathLog.SetQuantityResult(QuantityParser.CalculateWithUnits(input)))
                : new Action(() => mathLog.SetNumericResult(NumericParser.CalculateWithoutUnits(input)))
            )();

            MathLog.Add(mathLog);
            return mathLog;
        }
    }
}
