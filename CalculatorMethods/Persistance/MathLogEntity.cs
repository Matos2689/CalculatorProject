using System.Text.Json.Serialization;

namespace CalculatorProject.Persistance
{
    public class MathLogEntity
    {
        public string Expression { get; set; }
        public double ResultValue { get; set; }
        public string? ResultUnit { get; set; }

        public MathLogEntity() { }

        [JsonConstructor]
        public MathLogEntity(string expression, double resultValue, string? resultUnit = null)
        {
            Expression = expression;
            ResultValue = resultValue;
            ResultUnit = resultUnit;
        }
    }
}
