using CalculatorMethods.BusinessLogic;
using CalculatorMethods.Contracts;
using CalculatorMethods.Persistance;
using FluentAssertions;
using UnitsNet;

namespace CalculatorTests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void ShouldSaveWritesJsonHistoryFile()
        {

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
        public void ShouldReturnAMathLogEntityWithDoubleResult()
        {
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
        public void ShouldVerifyContentMathLogsEntitiesOnList()
        {
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
        public void ShouldCreateNumericMathLogItem()
        {

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
        public void ShouldCreateUnitMathLogItem(string expression, double numericResult, string unit)
        {

            // Arrange 
            var entity = new MathLogEntity(expression, numericResult, unit);

            // Act
            var item = entity.FromEntity();

            // Assert
            item.Expression.Should().Be(expression);
            item.Type.Should().Be(MathLogTypes.UnitBased);
            item.QuantityResult.Should().Be(Length.FromMeters(4));
            item.QuantityResult.ToString().Should().Be("4 m");
        }

        [TestMethod]
        public void Should_CreateMathLogItems_WithCorrectNumericAndUnitBasedResults()
        {
            // Arrange
            var logs = new List<MathLogEntity>
            {
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

        [TestMethod]
        public void ShouldLoadJsonFile()
        {
            // Arrange
            var jsonFile = new JsonRepositoryManager();
            const string fileName = "SaveMathlog.json";

            // Act
            jsonFile.Save(new List<MathLogItem>());

            // Assert
            File.Exists(fileName).Should().BeTrue();
            var logs = jsonFile.Load();
            logs.Should().NotBeNull();
            logs.Should().BeEmpty();

            // Cleanup
            File.Delete(fileName);
        }

        [TestMethod]
        public void ShouldReturnEmptyListWhenJsonFileIsMissing()
        {
            // Arrange
            var manager = new JsonRepositoryManager();
            var filePath = "SaveMathlog.json";
            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            var result = manager.Load();

            // Assert
            result.Should().BeEmpty();
            File.Exists(filePath).Should().BeFalse();
        }

        [TestMethod]
        public void ShouldReturnEmptyListWhenJsonFileContainsNull()
        {
            // Arrange
            var manager = new JsonRepositoryManager();
            var filePath = "SaveMathlog.json";
            File.WriteAllText(filePath, "null");

            // Act
            var result = manager.Load();

            // Assert
            result.Should().BeEmpty();

            // Cleanup
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        [TestMethod]
        public void ShouldReadJsonFileContent()
        {
            // Arrange
            var manager = new JsonRepositoryManager();
            const string fileName = "SaveMathlog.json";

            // Act
            manager.Save(new List<MathLogItem>());
            manager.Read();

            // Assert
            File.Exists(fileName).Should().BeTrue();
            var content = File.ReadAllText(fileName);
            content.Should().NotBeNullOrEmpty();

            // Cleanup
            File.Delete(fileName);
        }

        [TestMethod]
        public void ShoulDeserializeJsonFileContent()
        {
            // Arrange
            var mathLog1 = new MathLogItem("2+2");
            mathLog1.SetNumericResult(4);

            var mathLog2 = new MathLogItem("3m+3m");
            mathLog2.SetQuantityResult(Length.FromMeters(6));

            var mathLog3 = new MathLogItem("10m/2");
            mathLog3.SetQuantityResult(Ratio.FromDecimalFractions(5));

            var logs = new List<MathLogItem> { mathLog1, mathLog2, mathLog3 };
            var manager = new JsonRepositoryManager();
            manager.Save(logs);

            // Act
            var loadedLogs = manager.Load();

            // Assert
            loadedLogs.Should().HaveCount(3);
            loadedLogs[0].Expression.Should().Be("2+2");
            loadedLogs[0].NumericResult.Should().Be(4);
            loadedLogs[0].Type.Should().Be(MathLogTypes.NumericBased);
            loadedLogs[1].Expression.Should().Be("3m+3m");
            loadedLogs[1].QuantityResult.Should().Be(Length.FromMeters(6));
            loadedLogs[1].Type.Should().Be(MathLogTypes.UnitBased);
            loadedLogs[2].Expression.Should().Be("10m/2");
            loadedLogs[2].QuantityResult.Should().Be(Ratio.FromDecimalFractions(5));
            loadedLogs[2].Type.Should().Be(MathLogTypes.UnitBased);
        }

        [TestMethod]
        public void ShouldCreateAndLoadXmlFile()
        {
            const string FileName = "SaveMathlog.xml";

            // Arrange
            IRepository repo = new XmlRepositoryManager();

            // Act
            repo.Save(new List<MathLogItem>());

            // Assert
            File.Exists(FileName).Should().BeTrue();
            repo.Load().Should().BeEmpty();            

            // Cleanup
            if (File.Exists(FileName)) File.Delete(FileName);
        }

        [TestMethod]
        public void ShouldLoadReturnEmptyListWhenXmlFileIsMissing()
        {
            // Arrange
            var manager = new XmlRepositoryManager();
            var filePath = "SaveMathlog.xml";
            if (File.Exists(filePath)) File.Delete(filePath);

            // Act
            var result = manager.Load();

            // Assert
            result.Should().BeEmpty();
            File.Exists(filePath).Should().BeFalse();
        }
    }
}
