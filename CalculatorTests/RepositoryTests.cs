using CalculatorProject.BusinessLogic;
using CalculatorProject.Contracts;
using CalculatorProject.Persistance;
using FluentAssertions;
using UnitsNet;

namespace CalculatorTests
{
    [TestClass]
    public class RepositoryTests
    {
        private static readonly string _directory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data");

        [TestMethod]
        public void ShouldSaveWritesJsonHistoryFile()
        {
            // Arrange
            var expression = new List<string>
            {
                "2+2", "2m + 2m"
            };

            var repo = new JsonRepositoryManager();
            var calculator = new Calculator(repo);

            foreach (var exp in expression)
            {  
                calculator.Calculate(exp);
            }

            // Act
            string filePath = Path.Combine(_directory, "ShouldSaveWritesJsonHistoryFile.json");
            repo.Save(filePath);
            Console.WriteLine($"File saved at: {filePath}");

            // Assert
            File.Exists(filePath).Should().BeTrue();

            var json = File.ReadAllText(filePath);

            json.Should().Contain("\"Expression\": \"2+2\"");
            json.Should().Contain("\"ResultValue\": 4");
            json.Should().Contain("\"ResultUnit\": null");

            json.Should().Contain("\"Expression\": \"2m + 2m\"");
            json.Should().Contain("\"ResultValue\": 4");
            json.Should().Contain("\"ResultUnit\": \"Meter\"");
        }

        [TestMethod]
        public void ShouldReturnAMathLogEntityWithDoubleResult()
        {
            // Arrange
            var input = "2+2";
            var repositoryJson = new JsonRepositoryManager();
            var calculator = new Calculator(repositoryJson);
            calculator.Calculate(input);
            var mathLog = calculator.Memory.First();

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
            entities.Last().QuantityResult!.ToString().Should().Be("6 m");
        }

        [TestMethod]
        public void ShouldLoadJsonFile()
        {
            // Arrange
            var jsonFile = new JsonRepositoryManager();
            string filePath = $"{_directory}ShouldLoadJsonFile.json";

            // Act
            jsonFile.Save(filePath);

            // Assert
            File.Exists(filePath).Should().BeTrue();
            var logs = jsonFile.Memory;
            logs.Should().NotBeNull();
            logs.Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldReturnEmptyListWhenJsonFileIsMissing()
        {
            // Arrange
            var manager = new JsonRepositoryManager();
            var filePath = $"{_directory}ShouldReturnEmptyListWhenJsonFileIsMissing.json";

            // Act
            var result = manager.Memory;

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldReturnEmptyListWhenJsonFileContainsNull()
        {
            // Arrange
            var manager = new JsonRepositoryManager();
            var filePath = $"{_directory}ShouldReturnEmptyListWhenJsonFileContainsNull.json";
            File.WriteAllText(filePath, "null");

            // Act
            var result = manager.Memory;

            // Assert
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void ShouldReadJsonFileContent()
        {
            // Arrange
            var manager = new JsonRepositoryManager();
            string filePath = $"{_directory}ShouldReadJsonFileContent.json";

            // Act
            manager.Save(filePath);

            // Assert
            File.Exists(filePath).Should().BeTrue();
            var content = File.ReadAllText(filePath);
            content.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void ShoulDeserializeJsonFileContent()
        {
            // Arrange
            var expressions = new List<string>()
            {
                "2+2", "3m+3m", "10m/2"
            };

            IRepository repo = new JsonRepositoryManager();

            foreach (var expression in expressions)
            {
                var calculator = new Calculator(repo);
                calculator.Calculate(expression);
            }

            var filePath = $"{_directory}ShoulDeserializeJsonFileContent.json";
            
            repo.Save(filePath);
            repo.Memory.Clear();

            // Act
            repo.Load(filePath);
            var loadedLogs = repo.Memory;

            // Assert
            loadedLogs.Should().HaveCount(3);            
        }

        [TestMethod]
        public void ShouldLoadXmlFile()
        {
            // Arrange
            var expressions = new List<string>
            {
                "2+2", "3m+3m", "10m/2"
            };

            var repo = new XmlRepositoryManager();

            var calculator = new Calculator(repo);
            foreach (var expression in expressions)
            {
                calculator.Calculate(expression);
            }

            string filePath = $"{_directory}ShouldLoadXmlFile.xml";

            repo.Save(filePath);
            repo.Memory.Clear();

            // Act
            repo.Load(filePath);
            var loadedLogs = repo.Memory;

            // Assert
            loadedLogs.Should().HaveCount(3);
        }

        [TestMethod]
        public void ShouldCreateAndLoadXmlFile()
        {
            // Arrange
            var mathLog1 = new MathLogItem("2+2");
            mathLog1.SetNumericResult(4);

            var mathLog2 = new MathLogItem("3m+3m");
            mathLog2.SetQuantityResult(Length.FromMeters(6));

            var mathLog3 = new MathLogItem("10m/2");
            mathLog3.SetQuantityResult(Ratio.FromDecimalFractions(5));

            var logs = new List<MathLogItem> { mathLog1, mathLog2, mathLog3 };

            string filePath = $"{_directory}ShouldCreateAndLoadXmlFile.xml";

            IRepository repo = new XmlRepositoryManager();

            // Act
            repo.Save(filePath);

            // Assert
            File.Exists(filePath).Should().BeTrue();
        }

        [TestMethod]
        public void ShouldLoadReturnEmptyListWhenXmlFileIsMissing()
        {
            // Arrange
            var manager = new XmlRepositoryManager();
            var filePath = $"{_directory}ShouldLoadReturnEmptyListWhenXmlFileIsMissing.xml";

            // Act
            var result = manager.Memory;

            // Assert
            result.Should().BeEmpty();
            File.Exists(filePath).Should().BeFalse();
        }
    }
}
