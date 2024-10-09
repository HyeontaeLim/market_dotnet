namespace market.Domain;

public class OrderForm
{
    public long MemberId { get; set; }
    public List<OrderItemForm> OrderItemForms { get; set; }
    public string Address { get; set; }

    public OrderForm(long memberId, List<OrderItemForm> orderItemForms, string address)
    {
        MemberId = memberId;
        OrderItemForms = orderItemForms;
        Address = address;
    }
}

public class OrderItemForm
{
    public long ItemId { get; set; }
    public int OrderPrice { get; set; }
    public int Count { get; set; }

    public OrderItemForm(long itemId, int orderPrice, int count)
    {
        ItemId = itemId;
        OrderPrice = orderPrice;
        Count = count;
    }
}