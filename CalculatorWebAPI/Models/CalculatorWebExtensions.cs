using UnitsNet;

namespace CalculatorWebAPI.Models
{
    public static class CalculatorWebExtensions
    {
        public static string Abreviation(this IQuantity value)
        {
            var unitEnum = value.Unit;
            var unitType = unitEnum.GetType();
            var unitValue = Convert.ToInt32(unitEnum);

            var abbreviations = UnitAbbreviationsCache.Default.GetUnitAbbreviations(unitType, unitValue);

            return abbreviations.Length > 0 ? abbreviations[0] : string.Empty;
        }
    }
}
