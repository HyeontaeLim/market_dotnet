using System.ComponentModel.DataAnnotations;

namespace market.Domain;

public class Item
{
    public long ItemId { get; set; }
    public string? ItemName { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public string? Img { get; set; }
    public bool Status { get; set; }
    public long CategoryId { get; set; }

    public Item()
    {
    }

    public Item(long itemId, string itemName, int price, int quantity, string img, bool status, long categoryId)
    {
        ItemId = itemId;
        ItemName = itemName;
        Price = price;
        Quantity = quantity;
        Img = img;
        Status = status;
        CategoryId = categoryId;
    }
}