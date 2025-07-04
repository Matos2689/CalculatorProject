using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitsNet;

namespace CalculatorClasses;

[TestClass]
public class CalculatorTests {

    [TestMethod]
    [DataRow("2+2+2+2", 8)]
    [DataRow("50 + 50", 100)]
    public void ShouldAddNumbersBasedOnString(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().NumericResult.Should().Be(expectedResult);
    }

    [TestMethod]
    [DataRow("20 - 5", 15)]
    [DataRow("20-5", 15)]
    [DataRow("20-5-5-5", 5)]
    public void ShouldSubtractNumbersBasedOnStrings(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().NumericResult.Should()
            .Be(expectedResult);
    }

    [TestMethod]
    [DataRow("10 * 10", 100)]
    [DataRow("10*10", 100)]
    [DataRow("10*10*10*10", 10000)]
    public void ShouldMultiplyNumbersBasedOnStrings(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().NumericResult.Should()
            .Be(expectedResult);
    }

    [TestMethod]
    [DataRow("50 / 2", 25)]
    [DataRow("50/2", 25)]
    [DataRow("40/2/2/2", 5)]
    public void ShouldDivideNumbersBasedOnStrings(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().NumericResult.Should()
            .Be(expectedResult);
    }

    [TestMethod]
    [DataRow("15+15", 30)]
    [DataRow("15 + 15", 30)]
    [DataRow("5+5+5+5+5+5", 30)]
    [DataRow("30-15", 15)]
    [DataRow("30 - 15", 15)]
    [DataRow("30 - 10 - 10 - 5", 5)]
    [DataRow("10*10", 100)]
    [DataRow("2*2*2*2*2", 32)]
    [DataRow("10 * 10", 100)]
    [DataRow("50/2", 25)]
    [DataRow("50 / 2", 25)]
    [DataRow("60 / 2 / 2 / 5", 3)]
    [DataRow("5 / 2", 2.5)]
    [DataRow("5 * 20 / 4 + 5", 30)]
    [DataRow("10 + 20 / 2 + 5 * 2", 30)]
    [DataRow("10 - 20", -10)]
    [DataRow("-10 - 20", -30)]
    [DataRow("-10 * 2", -20)]
    public void ShouldDoCalculationsWithAnyOperator(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().NumericResult.Should()
            .Be(expectedResult);
    }

    [TestMethod]
    [DataRow("2m + 2m", 4)]
    [DataRow("15m + 15m", 30)]
    [DataRow("5 m + 5 m", 10)]
    [DataRow("10m + 5m", 15)]
    [DataRow("20m+5m", 25)]
    [DataRow("20m + 100mm", 20.1)]
    public void ShouldDoAddNumbersWithUnits(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().QuantityResult.Should()
            .Be(Length.FromMeters(expectedResult));
    }

    [TestMethod]
    [DataRow("15m - 5m", 10)]
    [DataRow("50m - 20m", 30)]
    [DataRow("10 m - 5 m", 5)]
    [DataRow("20m - 5m", 15)]
    [DataRow("8m - 2m", 6)]
    public void ShouldDoSubtractNumbersWithUnits(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().QuantityResult.Should()
            .Be(Length.FromMeters(expectedResult));
    }

    [TestMethod]
    [DataRow("5m * 5m", 25)]
    [DataRow("5 m * 5 m", 25)]
    [DataRow("5m * 25m", 125)]
    [DataRow("5 m * 25 m", 125)]
    public void ShouldDoMultiplyNumbersWithUnits(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().QuantityResult.Should()
            .Be(Area.FromSquareMeters(expectedResult));
    }

    [TestMethod]
    [DataRow("20m / 2m", 10)]
    [DataRow("10m/2m", 5)]
    [DataRow("10 m / 5 m", 2)]
    [DataRow("5m / 2m", 2.5)]
    public void ShouldDivideNumbersWithUnits(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().QuantityResult.Should()
            .Be(Ratio.FromDecimalFractions(expectedResult));
    }
}

