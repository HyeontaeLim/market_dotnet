using System.Text;
using market.DbConnection;
using market.Domain;
using Microsoft.Extensions.Primitives;
using MySql.Data.MySqlClient;

namespace market.Repository;

public class ItemRepositoryImpl : IItemRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<ItemRepositoryImpl> _logger;

    public ItemRepositoryImpl(IDbConnectionFactory connectionFactory, ILogger<ItemRepositoryImpl> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public Item SaveItem(Item item)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO item (item_name, price, quantity, img, status, category_id) values " +
                    "(@itemName, @price, @quantity, @img, @status, @categoryId); SELECT LAST_INSERT_ID();";
                command.Parameters.Add(new MySqlParameter("@itemName", item.ItemName));
                command.Parameters.Add(new MySqlParameter("@price", item.Price));
                command.Parameters.Add(new MySqlParameter("@quantity", item.Quantity));
                command.Parameters.Add(new MySqlParameter("@img", item.Img));
                command.Parameters.Add(new MySqlParameter("@status", item.Status));
                command.Parameters.Add(new MySqlParameter("@categoryId", item.CategoryId));
                
                long itemId = Convert.ToInt64(command.ExecuteScalar());
                item.ItemId = itemId;
            }
        }
        return item;
    }

    public IEnumerable<ItemDto> FindItemsAll(int page, int pageSize, SearchCon? searchCon)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            StringBuilder sqlBuilder =
                new StringBuilder("SELECT item_id, item_name, price, quantity, img, status, cat_name, c.category_id AS category_id FROM item I INNER JOIN category C ON I.category_id = C.category_id ");
            if(!(searchCon == null || (searchCon.Status == false && searchCon.CategoryId == null && String.IsNullOrWhiteSpace(searchCon.Keyword))))
            {
               sqlBuilder.Append("WHERE ");
               if (searchCon.Status)
               {
                   sqlBuilder.Append("status = true AND ");
               }

               if (!String.IsNullOrWhiteSpace(searchCon.Keyword))
               {
                   sqlBuilder.Append($"item_name LIKE '%{searchCon.Keyword}%' AND ");
               }
            
               if (searchCon.CategoryId != null && searchCon.CategoryId.Count!=0)
               {

                   sqlBuilder.Append($"c.category_id in ({string.Join(", ", searchCon.CategoryId)}) And ");
                   
               }
               sqlBuilder.Remove(sqlBuilder.Length - 5, 4);
            }

            string sql = sqlBuilder.ToString();

            command.CommandText =
                $"{sql}LIMIT @PageSize OFFSET @Offset";
            
            command.Parameters.Add(new MySqlParameter("@PageSize", pageSize));
            command.Parameters.Add(new MySqlParameter("@Offset", (page-1)*pageSize));
            _logger.LogInformation(command.CommandText);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return new ItemDto(
                        reader.GetInt64(0), 
                        reader.GetString(1), 
                        reader.GetInt32(2),
                        reader.GetInt32(3),
                        reader.GetString(4),
                        reader.GetBoolean(5),
                        reader.GetString(6),
                        reader.GetInt64(7)
                        );
                }
            }
        }

    }

    public IEnumerable<Item> FindByCat(Category category)
    {
        throw new NotImplementedException();
    }

    public Item? FindById(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT item_id, item_name, price, quantity, img, status, category_id FROM item WHERE item_id = @id";
            command.Parameters.Add(new MySqlParameter("@id", id));
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Item(
                        reader.GetInt64(0), 
                        reader.GetString(1), 
                        reader.GetInt32(2),
                        reader.GetInt32(3),
                        reader.GetString(4),
                        reader.GetBoolean(5),
                        reader.GetInt64(6)
                    );
                }
                return null;
            }
        }

    }

    public void DeleteById(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from item where item_id = @id";
                command.Parameters.Add(new MySqlParameter("@id", id));
                command.ExecuteNonQuery();
            }
        }
    }

    public void UpdateQuantityById(long id, int quantity)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE item SET quantity = @quantity WHERE item_id = @id";
                command.Parameters.Add(new MySqlParameter("@id", id));
                command.Parameters.Add(new MySqlParameter("@quantity", quantity));
                command.ExecuteNonQuery();
            }
        }

        if (quantity < 0)
        {
            throw new Exception("Item Quantity cannot be under zero");
        }
    }
}