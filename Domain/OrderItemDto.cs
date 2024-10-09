using K4os.Compression.LZ4.Engine;

namespace market.Domain;

public class OrderItemDto
{
    public string ItemName { get; set; }
    public long OrderItemId { get; set; }
    public int Price { get; set; }
    public long OrderId { get; set; }
    public int OrderPrice { get; set; }
    public int Count { get; set; }
    public string CatName { get; set; }

    public OrderItemDto()
    {
    }

    public OrderItemDto(string itemName, long orderItemId, int price, long orderId, int orderPrice, int count,
        string catName)
    {
        ItemName = itemName;
        OrderItemId = orderItemId;
        Price = price;
        OrderId = orderId;
        OrderPrice = orderPrice;
        Count = count;
        CatName = catName;
    }
}