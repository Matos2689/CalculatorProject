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
        public QuantityParser _quantityParser = new();
        public NumericParser _numericParser = new();

        public Calculator(IRepository repository)
        {
            _repository = repository;
        }
        // todo: don't return object, but a more specific type. 
        public MathLogItem Calculate(string input)
        {
            var mathLog = new MathLogItem(input);

            (input.Any(char.IsLetter)
                ? new Action(() => mathLog.SetQuantityResult(_quantityParser.CalculateWithUnits(input)))
                : new Action(() => mathLog.SetNumericResult(_numericParser.CalculateWithoutUnits(input)))
            )();

            Memory.Add(mathLog);
            return mathLog;
        }
    }
}
