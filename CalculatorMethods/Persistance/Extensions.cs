using System.Globalization;
using CalculatorMethods.Contracts;
using UnitsNet;

namespace CalculatorMethods.Persistance
{
    public static class Extensions
    {
        private static readonly Dictionary<string, Func<double, IQuantity>> _unitFactory = new()
        {
            { "Kilometer", value => Length.FromKilometers(value) },
            { "Meter", value => Length.FromMeters(value) },            
            { "Centimeter", value => Length.FromCentimeters(value) },
            { "Millimeter", value => Length.FromMillimeters(value) },
            { "CubicCentimeter", value => Volume.FromCubicCentimeters(value) },
            { "SquareMeter", value => Area.FromSquareMeters(value) },
            { "CubicMeter", value => Volume.FromCubicMeters(value) }
        };

        public static MathLogEntity ToEntity(this MathLogItem item) 
            
            => item.Type switch
        {
             MathLogTypes.NumericBased
                 => new MathLogEntity(item.Expression, item.NumericResult),

             MathLogTypes.UnitBased
                 => new MathLogEntity(
                        item.Expression,
                        (double)item.QuantityResult.Value,
                        item.QuantityResult.Unit.ToString()),

             _ => throw new InvalidOperationException(
                      "MathLogItem type is not initialized or invalid.")
         };


        public static IEnumerable<MathLogEntity> ToEntities(this List<MathLogItem> Items)

            // For each MathLogItem, convert it to MathLogEntity
            => Items.Select(item => item.ToEntity());


        public static MathLogItem FromEntity(this MathLogEntity entity)
        {
            var item = new MathLogItem(entity.Expression);

            if (_unitFactory.TryGetValue(entity.ResultUnit ?? "", out var factory))
                item.SetQuantityResult(factory(entity.ResultValue));
            else
                item.SetNumericResult(entity.ResultValue);

            return item;
        }


        public static IEnumerable<MathLogItem> FromEntities(this List<MathLogEntity> entities)

            // For each entity, convert it to MathLogItem
            => entities.Select(element => element.FromEntity());
        
    }
}
