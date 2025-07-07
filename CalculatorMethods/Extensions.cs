namespace CalculatorMethods {
    public static class Extensions {
        public static MathLogEntity ToEntity(this MathLogItem mathLogItem) {

            var expression = mathLogItem.Expression;
            double value;
            string? unit = null;

            if (mathLogItem.Type == MathLogTypes.NumericBased) {
                value = mathLogItem.NumericResult;

                return new MathLogEntity(expression, value);
            } 
            
            if (mathLogItem.Type == MathLogTypes.UnitBased){
                value = (double)mathLogItem.QuantityResult.Value;
                unit = mathLogItem.QuantityResult.Unit.ToString();

                return new MathLogEntity(expression, value, unit);
            }

            throw new InvalidOperationException("MathLogItem type is not initialized or invalid.");
        }

        public static IEnumerable<MathLogEntity> ToEntities(this List<MathLogItem> mathLogItems) {

            foreach (var item in mathLogItems) {
                yield return item.ToEntity();
            }
        }

        //public static MathLogItem FromEntity(this MathLogEntity mathLogEntity) {

        //}

        //public static List<MathLogItem> FromEntities(this List<MathLogEntity> mathLogEntities) {

        //}
    }
}
