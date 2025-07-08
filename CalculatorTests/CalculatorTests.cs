using System.Globalization;
using System.Text.Json;
using FluentAssertions;
using UnitsNet;
using CalculatorMethods;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests;

[TestClass]
public class CalculatorTests {

    [TestMethod]
    [DataRow("2+2+2+2", 8)]
    [DataRow("50 + 50", 100)]
    public void ShouldAddNumbersBasedOnString(string input, double expectedResult) {

        // Arrange
        Calculator _calculator = new Calculator();

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
        Calculator _calculator = new Calculator();

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
        Calculator _calculator = new Calculator();

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
        Calculator _calculator = new Calculator();

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
        Calculator _calculator = new Calculator();

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
    [DataRow("1m + 1m", 2)]
    [DataRow("2m + 2m", 4)]
    public void ShouldDoAddNumbersWithUnits(string input, double expectedResult) {

        // Arrange
        Calculator _calculator = new Calculator();

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
    [DataRow("2m - 1m", 1)]
    public void ShouldDoSubtractNumbersWithUnits(string input, double expectedResult) {

        // Arrange
        Calculator _calculator = new Calculator();

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
    [DataRow("5m * 25 m", 125)]
    public void ShouldDoMultiplyNumbersWithUnits(string input, double expectedResult) {

        // Arrange
        Calculator _calculator = new Calculator();

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
        Calculator _calculator = new Calculator();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().QuantityResult.Should()
            .Be(Ratio.FromDecimalFractions(expectedResult));
    }

    [TestMethod]
    [DataRow("2km + 1km", 3)]
    [DataRow("2km + 500m", 2.5)]
    [DataRow("0.5km + 0.5km", 1)]
    [DataRow("1km + 1000", 2)]
    public void ShouldReturnKilometers(string input, double expectedResult) {

        // Arrange
        Calculator _calculator = new Calculator();

        // Act
        _calculator.Calculate(input);

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().QuantityResult.Should()
            .Be(Length.FromKilometers(expectedResult));
    }

    [TestMethod]
    [DataRow("500mm + 1m", 1500)]
    public void ShouldReturnMilimeters(string input, double expectedResult) {
        // Arrange
        Calculator _calculator = new Calculator();

        // Act
        _calculator.Calculate(input);
        var value = Length.FromMeters(20);

        var unit = value.Unit;
        var number = value.Value;

        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().QuantityResult.Should()
            .Be(Length.FromMillimeters(expectedResult));
    }

    [TestMethod]
    public void ShouldThrowExceptionForInvalidOperator() {

        // Arrange
        Calculator _calculator = new Calculator();

        string input = "5m = 5";

        // Act
        Action act = () => _calculator.Calculate(input);

        // Assert
        act.Should().Throw<IndexOutOfRangeException>().WithMessage("Operator not found!");
    }

    [TestMethod]
    [DataRow("1mmm + 5m +")]
    [DataRow("2y + 1")]
    [DataRow("x + y")]
    public void ShouldThrowExceptionForInvalidFormat(string input) {

        // Arrange
        Calculator _calculator = new Calculator();

        // Act
        Action act = () => _calculator.Calculate(input);

        // Assert
        act.Should().Throw<FormatException>();
    }

    [TestMethod]
    [DataRow("2km + 1000", 3)]// return km
    [DataRow("20m * 2", 40)]// return m
    [DataRow("100cm + 100cm", 200)]// retun cm
    [DataRow("2m + 1000mm", 3)]// return mm

    public void ShouldCalculateIQuantityAndDoubleCombined(string input, double expectedResult) {
        // Arrange
        Calculator _calculator = new Calculator();

        // Act
        _calculator.Calculate(input);

        // Assert
        var log = _calculator.MathLog.First();
        log.Expression.Should().Be(input);
        var qty = log.QuantityResult;

        if (qty is Ratio rat) rat.DecimalFractions.Should().Be(expectedResult);

        else if (qty is Length len) len.Value.Should().Be(expectedResult);

        else if (qty is Area area) area.SquareMeters.Should().Be(expectedResult);
    }

    [TestMethod]
    public void ShouldSaveWritesJsonHistoryFile() {

        // Arrange
        var logs = new List<MathLogItem>();

        var numeric = new MathLogItem("2+2");
        numeric.SetNumericResult(4);
        logs.Add(numeric);

        var unit = new MathLogItem("2m + 2m");
        unit.SetQuantityResult(Length.FromMeters(4));
        logs.Add(unit);

        var sut = new JsonRepositoryManager();

        // Act
        sut.Save(logs);
        const string fileName = "SaveMathlog.json";

        // Assert
        File.Exists(fileName).Should().BeTrue();

        var json = File.ReadAllText(fileName);

        json.Should().Contain("\"Expression\": \"2+2\"");
        json.Should().Contain("\"ResultValue\": 4");
        json.Should().Contain("\"ResultUnit\": null");

        json.Should().Contain("\"Expression\": \"2m + 2m\"");
        json.Should().Contain("\"ResultValue\": 4");
        json.Should().Contain("\"ResultUnit\": \"Meter\"");

        // Cleanup
        File.Delete(fileName);
    }

    [TestMethod]
    public void ShouldReturnAMathLogEntityWithDoubleResult() {
        // Arrange
        var input = "2+2";
        var calculator = new Calculator();
        calculator.Calculate(input);
        var mathLog = calculator.MathLog.First();

        // Act
        var mathLogEntity = mathLog.ToEntity();

        // Assert
        mathLogEntity.Expression.Should().Be(input);
        mathLogEntity.ResultValue.Should().Be(4);
        mathLogEntity.ResultUnit.Should().BeNull();
    }

    [TestMethod]
    public void ShouldVerifyContentMathLogsEntitiesOnList() {
        // Arrange
        var logs = new List<MathLogItem> {
            new MathLogItem("2+2") { },
            new MathLogItem("3+3") { },
            new MathLogItem("5+2") { },
            new MathLogItem("5m+2m") { },
        };
        logs[0].SetNumericResult(4);
        logs[1].SetNumericResult(6);
        logs[2].SetNumericResult(7);
        logs[3].SetQuantityResult(Length.FromMeters(7));

        // Act        
        var result = logs.ToEntities().ToList();

        // Assert
        result.Should().HaveCount(4);
    }

    [TestMethod]
    public void ShouldCreateNumericMathLogItem() {

        // Arrange
        string expression = "2+2";
        double result = 4;

        var entity = new MathLogEntity(expression, result);

        // Act
        var item = entity.FromEntity();

        // Assert
        item.Expression.Should().Be(expression);
        item.NumericResult.Should().Be(result);
        item.Type.Should().Be(MathLogTypes.NumericBased);
        item.QuantityResult.Should().BeNull();
    }

    [TestMethod]
    [DataRow("2m+2m", 4, "Meter")]
    public void ShouldCreateUnitMathLogItem(string expression, double numericResult, string unit) {

        // Arrange 
        var entity = new MathLogEntity(expression, numericResult, unit);

        // Act
        var item = entity.FromEntity();

        // Assert
        item.Expression.Should().Be(expression);
        item.Type.Should().Be(MathLogTypes.UnitBased);
        item.QuantityResult.Should().Be(Length.FromMeters(4));//"4 m"
    }

    [TestMethod]
    public void Should()
    {
        // Arrange
        var logs = new List<MathLogEntity> {
            new MathLogEntity("2+2", 4),
            new MathLogEntity("3m+3m", 6, "Meter"),
        };

        // Act
        var entities = logs.FromEntities();

        // Assert
        entities.Should().HaveCount(2);
        entities.First().Expression.Should().Be("2+2");
        entities.First().NumericResult.Should().Be(4);
        entities.First().Type.Should().Be(MathLogTypes.NumericBased);

        entities.Last().Expression.Should().Be("3m+3m");
        entities.Last().Type.Should().Be(MathLogTypes.UnitBased);
        entities.Last().QuantityResult.Should().Be(Length.FromMeters(6));
        entities.Last().QuantityResult.ToString().Should().Be("6 m");
    }
}


