using System.Data;
using MySql.Data.MySqlClient;

namespace market.DbConnection;

public class MysqlDbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MysqlDbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}