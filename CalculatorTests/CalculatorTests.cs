using System.Net.Http.Headers;
using CalculatorProject.BusinessLogic;
using CalculatorProject.Contracts;
using CalculatorProject.Persistance;
using FluentAssertions;
using UnitsNet;

namespace CalculatorTests;

[TestClass]
public class CalculatorTests
{
    private IRepository _repository;
    public CalculatorTests()
    {
        var connectionString = 
            "Server=.;" +
            "Database=SQL_Calculator_DB;" +
            "Trusted_Connection=True;" +
            "Encrypt=False;";

        _repository = new SQLRepositoryManager(connectionString);
    }

    [TestMethod]
    [DataRow("2+2+2+2", 8)]
    [DataRow("50 + 50", 100)]
    public void ShouldAddNumbersBasedOnString(string input, double expectedResult)
    {
        // Arrange
        
        var calculator = new Calculator(_repository);

        // Act
        calculator.Calculate(input);

        // Assert
        calculator.Memory.First().Expression.Should().Be(input);
        calculator.Memory.First().NumericResult.Should().Be(expectedResult);
    }

    [TestMethod]
    [DataRow("20 - 5", 15)]
    [DataRow("20-5", 15)]
    [DataRow("20-5-5-5", 5)]
    public void ShouldSubtractNumbersBasedOnStrings(string input, double expectedResult)
    {
        // Arrange
        Calculator calculator = new Calculator(_repository);

        // Act
        calculator.Calculate(input);

        // Assert
        calculator.Memory.First().Expression.Should().Be(input);
        calculator.Memory.First().NumericResult.Should()
            .Be(expectedResult);
    }

    [TestMethod]
    [DataRow("10 * 10", 100)]
    [DataRow("10*10", 100)]
    [DataRow("10*10*10*10", 10000)]
    public void ShouldMultiplyNumbersBasedOnStrings(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.Memory.First().Expression.Should().Be(input);
        _calculator.Memory.First().NumericResult.Should()
            .Be(expectedResult);
    }

    [TestMethod]
    [DataRow("50 / 2", 25)]
    [DataRow("50/2", 25)]
    [DataRow("40/2/2/2", 5)]
    public void ShouldDivideNumbersBasedOnStrings(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.Memory.First().Expression.Should().Be(input);
        _calculator.Memory.First().NumericResult.Should()
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
    public void ShouldDoCalculationsWithAnyOperator(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.Memory.First().Expression.Should().Be(input);
        _calculator.Memory.First().NumericResult.Should()
            .Be(expectedResult);
    }

    [TestMethod]
    [DataRow("2m+2m+2m+2m+2m", 10)]
    [DataRow("15m + 15m", 30)]
    [DataRow("5 m + 5 m", 10)]
    [DataRow("10m + 5m", 15)]
    [DataRow("20m+5m", 25)]
    [DataRow("20m + 100mm", 20.1)]
    [DataRow("1m + 1m", 2)]
    [DataRow("2m + 2m", 4)]
    public void ShouldDoAddNumbersWithUnits(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.Memory.First().Expression.Should().Be(input);
        _calculator.Memory.First().QuantityResult.Should()
            .Be(Length.FromMeters(expectedResult));
    }

    [TestMethod]
    [DataRow("15m-5m-5m", 5)]
    [DataRow("50m - 20m", 30)]
    [DataRow("10 m - 5 m", 5)]
    [DataRow("20m - 5m", 15)]
    [DataRow("8m - 2m", 6)]
    [DataRow("2m - 1m", 1)]
    public void ShouldDoSubtractNumbersWithUnits(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.Memory.First().Expression.Should().Be(input);
        _calculator.Memory.First().QuantityResult.Should()
            .Be(Length.FromMeters(expectedResult));
    }

    [TestMethod]
    [DataRow("5m * 5m", 25)]
    [DataRow("5 m * 5 m", 25)]
    [DataRow("5m * 25m", 125)]
    [DataRow("5m * 25 m", 125)]
    public void ShouldDoMultiplyNumbersWithUnits(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.Memory.First().Expression.Should().Be(input);
        _calculator.Memory.First().QuantityResult.Should()
            .Be(Area.FromSquareMeters(expectedResult));
    }

    [TestMethod]
    [DataRow("20m / 2m", 10)]
    [DataRow("10m/2m", 5)]
    [DataRow("10 m / 5 m", 2)]
    [DataRow("5m / 2m", 2.5)]
    public void ShouldDivideNumbersWithUnits(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.Memory.First().Expression.Should().Be(input);
        _calculator.Memory.First().QuantityResult.Should()
            .Be(Ratio.FromDecimalFractions(expectedResult));
    }

    [TestMethod]
    [DataRow("2km + 1km", 3)]
    [DataRow("2km + 500m", 2.5)]
    [DataRow("0.5km + 0.5km", 1)]
    [DataRow("1km + 1000", 2)]
    public void ShouldReturnKilometers(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.Memory.First().Expression.Should().Be(input);
        _calculator.Memory.First().QuantityResult.Should()
            .Be(Length.FromKilometers(expectedResult));
    }

    [TestMethod]
    [DataRow("500mm + 1m", 1500)]
    public void ShouldReturnMilimeters(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        var value = Length.FromMeters(20);
        var unit = value.Unit;
        var number = value.Value;

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.Memory.First().Expression.Should().Be(input);
        _calculator.Memory.First().QuantityResult.Should()
            .Be(Length.FromMillimeters(expectedResult));
    }

    [TestMethod]
    public void ShouldThrowExceptionForInvalidOperator()
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        string input = "5m = 5";

        // Act
        Action act = () => _calculator.Calculate(input);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    [DataRow("1mmm + 5m +")]
    [DataRow("2y + 1")]
    [DataRow("x + y")]
    public void ShouldThrowExceptionForInvalidFormat(string input)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        Action act = () => _calculator.Calculate(input);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    [DataRow("2km + 1000", 3)]// return km
    [DataRow("20m * 2", 40)]// return m
    [DataRow("100cm + 100cm", 200)]// retun cm
    [DataRow("2m + 1000mm", 3)]// return mm

    public void ShouldCalculateIQuantityAndDoubleCombined(string input, double expectedResult)
    {
        // Arrange
        Calculator _calculator = new Calculator(_repository);

        // Act
        _calculator.Calculate(input);

        // Assert
        var log = _calculator.Memory.First();
        log.Expression.Should().Be(input);
        var qty = log.QuantityResult;

        if (qty is Ratio rat) rat.DecimalFractions.Should().Be(expectedResult);

        else if (qty is Length len) len.Value.Should().Be(expectedResult);

        else if (qty is Area area) area.SquareMeters.Should().Be(expectedResult);
    }

    [TestMethod]
    [DataRow("1m+50cm", 1.5)]
    [DataRow("35m-1000cm", 25)]
    [DataRow("1000m+500cm", 1005)]
    public void ShouldCalculateDifferentUnits(string input, double expectedResult)
    {
        // Arrange
        var calculator = new Calculator(_repository);
        calculator.Calculate(input);

        // Act
        var result = calculator.Memory.First();

        // Assert
        result.QuantityResult.Should().Be(Length.FromMeters(expectedResult));
    }

    [TestMethod]
    [DataRow("10m + 20m + 30m", 60)]
    public void ShouldAddThreeUnitValues(string input, double expectedResult)
    {
        // Arrange
        var calculator = new Calculator(_repository);
        calculator.Calculate(input);

        // Act
        var result = calculator.Memory.First();

        // Assert
        result.QuantityResult.Should().Be(Length.FromMeters(expectedResult));
    }

    [TestMethod]
    [DataRow("5dm * 10hm * 20dam", 100000)]
    public void ShouldMultiplyThreeUnitValues(string input, double expectedResult)
    {
        // Arrange
        var calculator = new Calculator(_repository);
        calculator.Calculate(input);

        // Act
        var result = calculator.Memory.First();

        // Assert
        result.QuantityResult.Should().Be(Volume.FromCubicMeters(expectedResult));
    }

    [TestMethod]
    public void ShouldAddLitersAndMilliliters()
    {
        // Arrange
        var calculator = new Calculator(_repository);

        string input = "1 + 1l";

        // Act
        calculator.Calculate(input);
        var result = calculator.Memory.First();

        // Assert
        result.QuantityResult.Should().Be(Volume.FromLiters(2));
    }

    [TestMethod]
    [DataRow("1kg + 1000g", 2)]
    public void ShouldCalculateUnitsMass(string input, double expectedResult)
    {
        // Arrange
        var calculator = new Calculator(_repository);

        // Act
        calculator.Calculate(input);
        var result = calculator.Memory.First();

        // Assert
        result.QuantityResult.Should().Be(Mass.FromKilograms(2));
    }

    [TestMethod]
    public void ShouldMakeTreatmentOfPontuation()
    {
        // Arrange
        var calculator = new Calculator(_repository);
        string input = "1,5m + 2,5m";

        // Act
        calculator.Calculate(input);
        var result = calculator.Memory.First();

        // Assert
        result.QuantityResult.Should().Be(Length.FromMeters(4));
    }

    [TestMethod]
    [DataRow("50% + 50%", 1)]
    [DataRow("50% * 10", 5)]
    [DataRow("50% 40", 20)]
    public void ShouldCalculatePercentage(string input, double expectedResul)
    {
        // Arrange
        var calculator = new Calculator(_repository);

        // Act
        calculator.Calculate(input);
        var result = calculator.Memory.First();

        // Assert
        result.NumericResult.Should().Be(expectedResul);
    }
}







