namespace market.Domain;

public class Order
{
    public long Id { get; set; }
    public long MemberId { get; set; }
    public string Address { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderStatus { get; set; }
    public string OrderNum { get; set; }

    public Order()
    {
    }

    public Order(long id, long memberId, string address, DateTime orderDate, string orderStatus, string orderNum)
    {
        Id = id;
        MemberId = memberId;
        Address = address;
        OrderDate = orderDate;
        OrderStatus = orderStatus;
        OrderNum = orderNum;
    }
}