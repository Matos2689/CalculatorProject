using UnitsNet;

namespace CalculatorClasses {
    public enum MathLogTypes {
        NotInitialized = 0,
        NumericBased = 1,
        UnitBased = 2
    }
    public class MathLogItem {

        public string Expression { get; private set; }
        public double NumericResult { get; private set; }
        public IQuantity QuantityResult { get; private set; }
        public MathLogTypes Type { get; private set; }

        public MathLogItem(string expression) {
            Expression = expression;
        }

        public void SetNumericResult(double numericResult) {
            NumericResult = numericResult;
            Type = MathLogTypes.NumericBased;
        }

        public void SetQuantityResult(IQuantity quantityResult) {
            QuantityResult = quantityResult;
            Type = MathLogTypes.UnitBased;
        }
    }
}