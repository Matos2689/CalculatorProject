using CalculatorProject.Contracts;

namespace CalculatorWebAPI.Models
{
    public class CalculationResponse
    {
        public string Expression { get; set; }
        public double? NumericResult { get; set; }
        public string? UnitResult { get; set; }

        public CalculationResponse(MathLogItem mathLog)
        {
            Expression = mathLog.Expression;

            if (mathLog.Type == MathLogTypes.NumericBased)
            {
                NumericResult = mathLog.NumericResult;
            }
            else if(mathLog.Type == MathLogTypes.UnitBased && mathLog.QuantityResult is not null)
            {
                UnitResult = mathLog.QuantityResult.ToString();
            }
        }
    }
}
