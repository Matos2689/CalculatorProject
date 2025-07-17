using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorProject.Persistance;
using UnitsNet;
using FluentAssertions;

namespace CalculatorTests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void ShouldFromEntityParseKilometers()
        {
            // Arrange
            string input = "1km + 1km";
            double expectedResult = 2;
            string unit = "Kilometer";

            var entity = new MathLogEntity(input, expectedResult, unit);

            // Act
            var mathLogItem = entity.FromEntity();

            // Assert
            mathLogItem.Expression.Should().Be("1km + 1km");
            mathLogItem.QuantityResult.Should().Be(Length.FromKilometers(2));
        }

        [TestMethod]
        [DataRow("1cm + 1cm", 2, "Centimeter")]
        public void ShouldFromEntityParseCentimeters
            (string input, double expectedResult, string unit)
        {
            // Arrange
            var entity = new MathLogEntity(input, expectedResult, unit);

            // Act
            var mathLogItem = entity.FromEntity();

            // Assert
            mathLogItem.Expression.Should().Be(input);
            mathLogItem.QuantityResult.Should().Be(Length.FromCentimeters(2));
        }

        [TestMethod]
        [DataRow("1mm + 1mm", 2, "Millimeter")]
        public void ShouldFromEntityParseMillimeters
            (string input, double expectedResult, string unit)
        {
            // Arrange
            var entity = new MathLogEntity(input, expectedResult, unit);

            // Act
            var mathLogItem = entity.FromEntity();

            // Assert
            mathLogItem.Expression.Should().Be(input);
            mathLogItem.QuantityResult.Should().Be(Length.FromMillimeters(2));
        }

        [TestMethod]
        [DataRow("1cm + 1cm", 2, "CubicCentimeter")]
        public void ShouldFromEntityParseCubicCentimeters
            (string input, double expectedResult, string unit)
        {
            // Arrange
            var entity = new MathLogEntity(input, expectedResult, unit);

            // Act
            var mathLogItem = entity.FromEntity();

            // Assert
            mathLogItem.Expression.Should().Be(input);
            mathLogItem.QuantityResult.Should().Be(Volume.FromCubicCentimeters(2));
        }

        [TestMethod]
        [DataRow("2m * 2m", 4, "SquareMeter")]
        public void ShouldFromEntityParseSquareMeters
            (string input, double expectedResult, string unit)
        {
            // Arrange
            var entity = new MathLogEntity(input, expectedResult, unit);

            // Act
            var mathLogItem = entity.FromEntity();

            // Assert
            mathLogItem.Expression.Should().Be(input);
            mathLogItem.QuantityResult.Should().Be(Area.FromSquareMeters(4));
        }

        [TestMethod]
        [DataRow("2m * 2m *2m", 8, "CubicMeter")]
        public void ShouldFromEntityParseCubicMeters
            (string input, double expectedResult, string unit)
        {
            // Arrange
            var entity = new MathLogEntity(input, expectedResult, unit);

            // Act
            var mathLogItem = entity.FromEntity();

            // Assert
            mathLogItem.Expression.Should().Be(input);
            mathLogItem.QuantityResult.Should().Be(Volume.FromCubicMeters(8));
        }
    }
}
