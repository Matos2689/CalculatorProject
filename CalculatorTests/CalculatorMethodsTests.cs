using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
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
    [DataRow("1m + 1m", 2)]    
    [DataRow("2m + 2m", 4)]    
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
    [DataRow("2m - 1m", 1)]
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
    [DataRow("5m * 25 m", 125)]
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

    [TestMethod]
    [DataRow("2km + 1km", 3)]
    [DataRow("2km + 500m", 2.5)]
    [DataRow("0.5km + 0.5km", 1)]
    [DataRow("1km + 1000", 2)]
    public void ShouldReturnKilometers(string input, double expectedResult) {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

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
        CalculatorMethods _calculator = new CalculatorMethods();
        
        // Act
        _calculator.Calculate(input);
        // Assert
        _calculator.MathLog.First().Expression.Should().Be(input);
        _calculator.MathLog.First().QuantityResult.Should()
            .Be(Length.FromMillimeters(expectedResult));
    }

    [TestMethod]
    public void ShouldThrowExceptionForInvalidOperator() {

        // Arrange
        CalculatorMethods _calculator = new CalculatorMethods();

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
        CalculatorMethods _calculator = new CalculatorMethods();
        
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
        CalculatorMethods _calculator = new CalculatorMethods();

        // Act
        _calculator.Calculate(input);

        // Assert
        var log = _calculator.MathLog.First();
        log.Expression.Should().Be(input);
        var qty = log.QuantityResult;

        if (qty is Ratio rat) rat.DecimalFractions.Should().Be(expectedResult);

        else if (qty is Length len) len.Value.Should().Be(expectedResult);

        else if(qty is Area area) area.SquareMeters.Should().Be(expectedResult);
    }

    [TestMethod]
    public void SholdSaveHistoryJsonCreateJsonFileWithExpectedContent() {

        // Arrange
        var logs = new List<MathLogItem>();

        var numeric = new MathLogItem("2+2");
        numeric.SetNumericResult(4);
        logs.Add(numeric);

        var unit = new MathLogItem("1m+1m");
        unit.SetQuantityResult(Length.Parse("2 m", CultureInfo.InvariantCulture));
        logs.Add(unit);

        // Change to temporary directory
        var originalDir = Directory.GetCurrentDirectory();
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        Directory.SetCurrentDirectory(tempDir);

        try {
            // Act Save JSON
            var sut = new JsonHistoryManager();
            sut.SaveHistoryJson(logs);

            // Assert
            const string fileName = "SaveMathlog.json";
            File.Exists(fileName).Should().BeTrue();

            // Reads and parses content
            var jsonContent = File.ReadAllText(fileName);
            using var doc = JsonDocument.Parse(jsonContent);
            var root = doc.RootElement;
            root.ValueKind.Should().Be(JsonValueKind.Array);
            root.GetArrayLength().Should().Be(2);

            // Verify first item (NumericBased)
            var first = root[0];
            first.GetProperty("Expression").GetString().Should().Be("2+2");
            first.GetProperty("Type").GetString().Should().Be("NumericBased");
            first.GetProperty("Result").GetDouble().Should().Be(4);

            // Verify second item (UnitBased)
            var second = root[1];
            second.GetProperty("Expression").GetString().Should().Be("1m+1m");
            second.GetProperty("Type").GetString().Should().Be("UnitBased");
            second.GetProperty("Result").GetString().Should().Be("2 m");

        } finally {
            // Cleanup
            Directory.SetCurrentDirectory(originalDir);
            Directory.Delete(tempDir, recursive: true);
        }
    }
}


