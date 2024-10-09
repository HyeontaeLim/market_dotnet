namespace market.Domain;

public class OrderItem
{
    public long Id { get; set; }
    public long ItemId { get; set; }
    public long OrderId { get; set; }
    public int OrderPrice { get; set; }
    public int Count { get; set; }

    public OrderItem()
    {
    }

    public OrderItem(long id, long itemId, long orderId, int orderPrice, int count)
    {
        Id = id;
        ItemId = itemId;
        OrderId = orderId;
        OrderPrice = orderPrice;
        Count = count;
    }
}