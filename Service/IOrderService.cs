using market.Domain;

namespace market.Service;

public interface IOrderService
{ 
    Dictionary<String, Object> addOrder(List<OrderItem> orderItems, Order order);
}