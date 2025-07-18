namespace CalculatorWebAPI.Models
{
    public class CalculationResponse
    {
        public string? Expression { get; set; }
        public double? NumericResult { get; set; }
        public string? UnitResult { get; set; }
    }
}
