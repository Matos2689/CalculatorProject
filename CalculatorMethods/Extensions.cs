using System.Globalization;
using UnitsNet;

namespace CalculatorMethods
{
    public static class Extensions
    {

        private static readonly Dictionary<string, Func<double, IQuantity>> _unitFactory = new()
        {
            { "Meter", value => Length.FromMeters(value) },
            { "Kilometer", value => Length.FromKilometers(value) },
            { "Centimeter", value => Length.FromCentimeters(value) },
            { "Millimeter", value => Length.FromMillimeters(value) },
            { "CubicCentimeter", value => Volume.FromCubicCentimeters(value) },
            { "SquareMeter", value => Area.FromSquareMeters(value) },
            { "CubicMeter", value => Volume.FromCubicMeters(value) }
        };

        public static MathLogEntity ToEntity(this MathLogItem mathLogItem)
        {

            var expression = mathLogItem.Expression;
            double value;
            string? unit = null;

            if (mathLogItem.Type == MathLogTypes.NumericBased)
            {
                value = mathLogItem.NumericResult;

                return new MathLogEntity(expression, value);
            }

            if (mathLogItem.Type == MathLogTypes.UnitBased)
            {
                value = (double)mathLogItem.QuantityResult.Value;
                unit = mathLogItem.QuantityResult.Unit.ToString();

                return new MathLogEntity(expression, value, unit);
            }

            throw new InvalidOperationException("MathLogItem type is not initialized or invalid.");
        }

        public static IEnumerable<MathLogEntity> ToEntities(this List<MathLogItem> Items)
        {
            foreach (var item in Items)
            {
                yield return item.ToEntity();
            }
        }

        public static MathLogItem FromEntity(this MathLogEntity entity)
        {
            var item = new MathLogItem(entity.Expression);

            if (!string.IsNullOrWhiteSpace(entity.ResultUnit)
                && _unitFactory.TryGetValue(entity.ResultUnit, out var factory))
            {
                var quantity = factory(entity.ResultValue);
                item.SetQuantityResult(quantity);
            }
            else
            {
                item.SetNumericResult(entity.ResultValue);
            }

            return item;
        }

        public static IEnumerable<MathLogItem> FromEntities(this List<MathLogEntity> entities)
        {
            foreach (var entity in entities)
            {
                yield return entity.FromEntity();
            }
        }
    }
}
