using System.Text.Json.Serialization;

namespace CalculatorMethods.Persistance
{
    public class MathLogEntity
    {
        public string Expression { get; set; }
        public double ResultValue { get; set; }
        public string? ResultUnit { get; set; }

        /// Parameterless constructor for deserialization xml
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
