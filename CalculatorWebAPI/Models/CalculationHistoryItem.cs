namespace CalculatorWebAPI.Models
{
    public class CalculationHistoryItem
    {
        public string? Expression { get; set; }
        public double? NumericResult { get; set; }
        public string? UnitResult { get; set; }
    }
}
