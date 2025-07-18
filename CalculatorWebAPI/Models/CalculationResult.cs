using CalculatorProject.Contracts;
using UnitsNet;

namespace CalculatorWebAPI.Models
{
    public class CalculationResult
    {
        public string Expression { get; set; }
        public double? NumericResult { get; set; }
        public IQuantity QuantityResult { get; set; }
        public string Type { get; set; }
        public CalculationResult(MathLogItem item)
        {
            Expression = item.Expression;
            Type = item.Type.ToString();

            if (item.Type == MathLogTypes.NumericBased)
            {
                NumericResult = item.NumericResult;
            }
            else if (item.Type == MathLogTypes.UnitBased && item.QuantityResult is not null)
            {
                QuantityResult = item.QuantityResult;
            }
        }
    }
}
