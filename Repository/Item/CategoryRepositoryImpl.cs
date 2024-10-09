using market.DbConnection;
using market.Domain;

namespace market.Repository;

public class CategoryRepositoryImpl : ICategoryRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CategoryRepositoryImpl(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Category? FindById(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT category_id, cat_name FROM CATEGORY where category_id = @id";
            using (var reader = command.ExecuteReader())
            {
                return reader.Read() ? new Category(reader.GetInt64(0), reader.GetString(1)) : null;
            }
        }
    }

    public IEnumerable<Category> FindAllCategory(string catName)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT category_id, cat_name FROM CATEGORY";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return new Category(reader.GetInt64(0), reader.GetString(1));
                }
            }
        }
    }
}