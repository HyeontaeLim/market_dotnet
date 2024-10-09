using System.Text;
using market.Domain;
using market.Filter;
using market.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace market.Controller;

[Route("[controller]")]
public class OrderController : Microsoft.AspNetCore.Mvc.Controller
{

    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }
    
    [HttpPost("/order"),Consumes("application/json; charset=UTF-8")]
    [ServiceFilter(typeof(LoginFilter))]
    public IActionResult AddOrder([FromBody] OrderForm orderForm)
    {
        StringBuilder orderNumBuilder = new StringBuilder();
        orderNumBuilder.Append(DateTime.Today.ToString("yyyyMMdd"));
        Guid guid = Guid.NewGuid();
        int hashCode = guid.GetHashCode();
        var randomNum = (Math.Abs(hashCode) % 100000000).ToString("D8");
        orderNumBuilder.Append(randomNum);
        Order order = new Order
        {
            MemberId = orderForm.MemberId,
            Address = orderForm.Address,
            OrderDate = DateTime.Now,
            OrderStatus = "결제 준비",
            OrderNum = orderNumBuilder.ToString()
        };

        List<OrderItemForm> orderItemForms = orderForm.OrderItemForms;
        List<OrderItem> orderItems = new List<OrderItem>();
        foreach (var orderItemForm in orderItemForms)
        {
            OrderItem orderItem = new OrderItem
            {
                ItemId = orderItemForm.ItemId,
                Count = orderItemForm.Count,
                OrderPrice = orderItemForm.OrderPrice
            };
            orderItems.Add(orderItem);
        }

        var addOrder = _orderService.addOrder(orderItems, order);

        return Ok(addOrder);
    }

}