using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMethods {
    public class MathLogEntity {
        public string Expression { get; private set; }
        public double Result { get; private set; }
        public string? Unit { get; private set; }

        public MathLogEntity(string expression, double result, string? resultUnit = null) {
            Expression = expression;
            Result = result;
            Unit = resultUnit;
        }
    }
}
