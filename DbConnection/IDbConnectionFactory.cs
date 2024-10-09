using System.Data;
namespace market.DbConnection;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}