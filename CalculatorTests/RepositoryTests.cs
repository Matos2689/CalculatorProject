using CalculatorMethods.BusinessLogic;
using CalculatorMethods.Contracts;
using CalculatorMethods.Persistance;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using UnitsNet;
using Dapper;

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
            var logs = new List<MathLogItem>();

            var numeric = new MathLogItem("2+2");
            numeric.SetNumericResult(4);
            logs.Add(numeric);

            var unit = new MathLogItem("2m + 2m");
            unit.SetQuantityResult(Length.FromMeters(4));
            logs.Add(unit);
            
            var sut = new JsonRepositoryManager();

            // Act
            string filePath = Path.Combine(_directory, "ShouldSaveWritesJsonHistoryFile.json");
            sut.Save(logs, filePath);
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
            string filePath = $"{_directory}ShouldLoadJsonFile.json";

            // Act
            jsonFile.Save(new List<MathLogItem>(), filePath);

            // Assert
            File.Exists(filePath).Should().BeTrue();
            var logs = jsonFile.Load(filePath);
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
            var result = manager.Load(filePath);

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
            var result = manager.Load(filePath);

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
            manager.Save(new List<MathLogItem>(), filePath);
            manager.Read(filePath);

            // Assert
            File.Exists(filePath).Should().BeTrue();
            var content = File.ReadAllText(filePath);
            content.Should().NotBeNullOrEmpty();
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

            var filePath = $"{_directory}ShoulDeserializeJsonFileContent.json";

            var manager = new JsonRepositoryManager();
            manager.Save(logs, filePath);
            logs.Clear();

            // Act
            var loadedLogs = manager.Load(filePath);

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
            repo.Save(logs, filePath);

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
            var result = manager.Load(filePath);

            // Assert
            result.Should().BeEmpty();
            File.Exists(filePath).Should().BeFalse();
        }

        [TestMethod]
        public void ShouldLoadXmlFile()
        {
            // Arrange
            var mathLog1 = new MathLogItem("2+2");
            mathLog1.SetNumericResult(4);

            var mathLog2 = new MathLogItem("3m+3m");
            mathLog2.SetQuantityResult(Length.FromMeters(6));

            var mathLog3 = new MathLogItem("10m/2");
            mathLog3.SetQuantityResult(Ratio.FromDecimalFractions(5));

            var logs = new List<MathLogItem> { mathLog1, mathLog2, mathLog3 };

            string filePath = $"{_directory}ShouldLoadXmlFile.xml";

            IRepository repo = new XmlRepositoryManager();

            repo.Save(logs, filePath); // Save xml

            // Act
            var loadedLogs = repo.Load(filePath); // Load xml

            // Assert
            loadedLogs.Should().HaveCount(3);
        }

        [TestMethod]
        public void Should_Save_InsertAllLogsIntoDatabase()
        {
            // Arrange
            const string connStr =
                "Server=.;Database=SQL_Calculator_DB;Trusted_Connection=True;Encrypt=False;";

            // Clear the MathLog table before testing
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlMapper.Execute(conn, "TRUNCATE TABLE dbo.MathLog");
            }

            var repo = new AdoNetRepositoryManager(connStr);

            var item1 = new MathLogItem("7+7");
            item1.SetNumericResult(14);

            var item2 = new MathLogItem("5*5");
            item2.SetNumericResult(25);

            var item3 = new MathLogItem("10m + 10m");
            item3.SetNumericResult(15);
            item3.SetQuantityResult(Length.FromMeters(20));

            var logs = new List<MathLogItem> { item1, item2, item3 };

            // Act
            repo.Save(logs, null);

            // Assert
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                int count = SqlMapper.QuerySingle<int>(conn, "SELECT COUNT(*) FROM dbo.MathLog");
                Assert.AreEqual(logs.Count, count);
            }
        }

        [TestMethod]
        public void Should_Load_ReturnExistingLogsFromDatabase()
        {
            // Arrange
            const string connStr =
                "Server=.;Database=SQL_Calculator_DB;Trusted_Connection=True;Encrypt=False;";
            var repo = new AdoNetRepositoryManager(connStr);

            // Act
            var logs = repo.Load(null);

            // Assert
            Assert.IsNotNull(logs);
            Assert.AreEqual(3, logs.Count);

            var log1 = logs.Single(l => l.Expression == "7+7");
            Assert.AreEqual(MathLogTypes.NumericBased, log1.Type);
            Assert.AreEqual(14, log1.NumericResult);

            var log2 = logs.Single(l => l.Expression == "5*5");
            Assert.AreEqual(MathLogTypes.NumericBased, log2.Type);
            Assert.AreEqual(25, log2.NumericResult);

            var log3 = logs.Single(l => l.Expression == "10m + 10m");
            Assert.AreEqual(MathLogTypes.UnitBased, log3.Type);
            Assert.AreEqual(Length.FromMeters(20), log3.QuantityResult);
        }
    }
}
