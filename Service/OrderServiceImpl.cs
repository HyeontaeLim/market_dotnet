using System.Transactions;
using market.DbConnection;
using market.Domain;
using market.Repository;

namespace market.Service;

public class OrderServiceImpl : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<OrderServiceImpl> _logger;

    public OrderServiceImpl(IOrderRepository orderRepository, ILogger<OrderServiceImpl> logger, IItemRepository itemRepository)
    {
        _orderRepository = orderRepository;
        _logger = logger;
        _itemRepository = itemRepository;
    }

    public Dictionary<String, Object> addOrder(List<OrderItem> orderItems, Order order)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
                try
                {
                    var addedOrder = _orderRepository.AddOrder(order);
                    var addedOrderId = addedOrder.Id;
                    foreach (var orderItem in orderItems)
                    {
                        Item foundItem = _itemRepository.FindById(orderItem.ItemId)!;
                        orderItem.OrderId = addedOrderId;
                        _orderRepository.AddOrderItem(orderItem);
                        _itemRepository.UpdateQuantityById(orderItem.ItemId, foundItem.Quantity-orderItem.Count);
                    }

                    List<OrderItemDto> orderItemDtos = _orderRepository.FindOrderItemByOrderId(addedOrderId).ToList();

                    var totalPrice = _orderRepository.GetTotalPrice(addedOrderId);
                    order.OrderStatus = "주문 완료";
                    _orderRepository.ChangeOrderStatus(order);
                    scope.Complete();
                    var dictionary = new Dictionary<String, Object>();
                    dictionary.Add("order", order);
                    dictionary.Add("totalPrice", totalPrice!);
                    dictionary.Add("orderItemDtos", orderItemDtos);
                    return dictionary;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw;
                }
        }
    }
}