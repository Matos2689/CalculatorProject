using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CalculatorMethods {    
    public class MathLogEntity {
        public string Expression { get; private set; }
        public double ResultValue { get; private set; }
        public string? ResultUnit { get; private set; }

        [JsonConstructor]
        public MathLogEntity(
            string expression, 
            double resultValue, 
            string? resultUnit = null)             
        {
            Expression = expression;
            ResultValue     = resultValue;
            ResultUnit       = resultUnit;
        }
    }
}
