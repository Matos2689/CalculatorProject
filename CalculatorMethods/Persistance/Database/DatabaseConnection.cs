namespace CalculatorProject.Persistance.Database;
using Microsoft.Data.SqlClient;

public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection()
    {
        _connectionString =
            "Server=.;" +
            "Database=SQL_Calculator_DB;" +
            "Trusted_Connection=True;" +
            "Encrypt=False;";
    }

    public SqlConnection Connect()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}
