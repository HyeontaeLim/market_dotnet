using market.DbConnection;
using market.Domain;
using MySql.Data.MySqlClient;

namespace market.Repository;

public class OrderRepositoryImpl : IOrderRepository
{
    
    private readonly IDbConnectionFactory _connectionFactory;

    public OrderRepositoryImpl(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public OrderItem AddOrderItem(OrderItem orderItem)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO order_item (item_id, order_id, order_price, count) values " +
                    "(@itemId, @orderId, @orderPrice, @count); SELECT LAST_INSERT_ID();";
                command.Parameters.Add(new MySqlParameter("@itemId", orderItem.ItemId));
                command.Parameters.Add(new MySqlParameter("@orderId", orderItem.OrderId));
                command.Parameters.Add(new MySqlParameter("@orderPrice", orderItem.OrderPrice));
                command.Parameters.Add(new MySqlParameter("@count", orderItem.Count));
                
                long orderItemId = Convert.ToInt64(command.ExecuteScalar());
                orderItem.Id = orderItemId;
            }
        }
        return orderItem;
    }

    public int? GetTotalPrice(long orderId)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT SUM(order_price) as total_price FROM order_item WHERE order_id = @id";
            command.Parameters.Add(new MySqlParameter("@id", orderId));
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }

                return null;
            }
        }
    }

    public Order ChangeOrderStatus(Order order)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "UPDATE `order` SET order_status = @status WHERE id = @id";
                command.Parameters.Add(new MySqlParameter("@status", order.OrderStatus));
                command.Parameters.Add(new MySqlParameter("@id", order.Id));
                command.ExecuteNonQuery();
            }
        }

        return order;
    }

    public IEnumerable<OrderItemDto> FindOrderItemByOrderId(long id)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText="SELECT item_name, order_item_id, price, order_id, order_price, count, cat_name FROM category C INNER JOIN " +
                                "(SELECT item_name, O.id as order_item_id, price, category_id, order_id, order_price, count FROM item I INNER JOIN order_item O ON I.item_id=O.item_id) " +
                                "AS T ON C.category_id=T.category_id WHERE order_id = @OrderId";
            command.Parameters.Add(new MySqlParameter("@OrderId", id));
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return new OrderItemDto(
                        reader.GetString(0),
                        reader.GetInt64(1),
                        reader.GetInt32(2),
                        reader.GetInt64(3),
                        reader.GetInt32(4),
                        reader.GetInt32(5),
                        reader.GetString(6)
                    );
                }
            }
        }
    }

    public Order AddOrder(Order order)
    {
        using (var connection = _connectionFactory.CreateConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO `order` (member_id, address, order_date, order_status, order_num) values " +
                    "(@memberId, @address, @orderDate, @orderStatus, @orderNum); SELECT LAST_INSERT_ID();";
                command.Parameters.Add(new MySqlParameter("@memberId", order.MemberId));
                command.Parameters.Add(new MySqlParameter("@address", order.Address));
                command.Parameters.Add(new MySqlParameter("@orderDate", order.OrderDate));
                command.Parameters.Add(new MySqlParameter("@orderStatus", order.OrderStatus));
                command.Parameters.Add(new MySqlParameter("@orderNum", order.OrderNum));
                
                long orderId = Convert.ToInt64(command.ExecuteScalar());
                order.Id = orderId;
            }
        }

        return order;
    }
}