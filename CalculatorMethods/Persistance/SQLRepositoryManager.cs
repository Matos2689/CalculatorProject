using System.Data;
using CalculatorProject.Contracts;
using CalculatorProject.Persistance.Database;
using Microsoft.Data.SqlClient;

namespace CalculatorProject.Persistance
{
    public class SQLRepositoryManager : IRepository
    {
        private readonly DatabaseConnection _databaseConnection;
        public List<MathLogItem> Memory { get; } = new List<MathLogItem>();
        public SQLRepositoryManager()
        {
            _databaseConnection = new DatabaseConnection();
        }

        public void Save(string _)
        {
            var entities = Memory.Select(l => l.ToEntity());

            using var conn = _databaseConnection.Connect();

            // Prepare the command SQL to insert data
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                                INSERT INTO dbo.MathLog(Expression, ResultValue, ResultUnit)
                                VALUES (@Expression, @ResultValue, @ResultUnit)";
            cmd.CommandType = CommandType.Text;

            // Add parameters to the command
            var pExpress = cmd.Parameters.Add("@Expression", SqlDbType.NVarChar, 255);
            var pValue = cmd.Parameters.Add("@ResultValue", SqlDbType.Float);
            var pUnit = cmd.Parameters.Add("@ResultUnit", SqlDbType.NVarChar, 100);

            foreach (var entity in entities)
            {
                pExpress.Value = entity.Expression;
                pValue.Value = entity.ResultValue;
                pUnit.Value = (object?)entity.ResultUnit ?? DBNull.Value;

                // Execute the command for each entity
                cmd.ExecuteNonQuery();
            }
        }

        public void Load(string _)
        {
            var result = new List<MathLogItem>();

            // Open the connection to the database
            using var conn = new SqlConnection();
            conn.Open();

            // Prepare the command SQL to select data
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT Expression, ResultValue, ResultUnit FROM dbo.MathLog";
            cmd.CommandType = CommandType.Text;

            // Execute the command and read the data
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Read the columns by index
                var expression = reader.GetString(0);
                var resultValue = reader.GetDouble(1);
                var resultUnit = reader.IsDBNull(2) ? null : reader.GetString(2);

                // Create a new MathLogItem and add it to the result list
                var entity = new MathLogEntity(expression, resultValue, resultUnit);
                result.Add(entity.FromEntity());
            }

            Memory.AddRange(result);
        }
    }
}
