using CalculatorProject.Contracts;

namespace CalculatorWebAPI.Models
{
    public class CalculationResult
    {
        public string Expression { get; set; }
        public string? NumericResult { get; set; }
        public string? QuantityResult { get; set; }
        public CalculationResult(MathLogItem item)
        {
            Expression = item.Expression;

            if (item.Type == MathLogTypes.NumericBased)
            {
                NumericResult = item.NumericResult.ToString();
            }
            else if (item.Type == MathLogTypes.UnitBased && item.QuantityResult is not null)
            {
                QuantityResult = $"{item.QuantityResult.Value}{item.QuantityResult.Abreviation()}";
            }
        }
    }
}