using market.Domain;

namespace market.Repository;

public interface IOrderRepository
{
    OrderItem AddOrderItem(OrderItem orderItem);
    int? GetTotalPrice(long orderId);
    Order ChangeOrderStatus(Order order);
    IEnumerable<OrderItemDto> FindOrderItemByOrderId(long id);
    Order AddOrder(Order order);
}