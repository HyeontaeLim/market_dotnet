using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace market.Domain;

public class ItemForm 
{
    public long ItemId { get; set; }
    [Required(ErrorMessage = "상품명을 입력해주세요.")]
    public string ItemName { get; set; }
    [Required(ErrorMessage = "가격을 입력해주세요.")]
    public int? Price { get; set; }
    [Required(ErrorMessage = "수량을 입력해주세요.")]
    public int? Quantity { get; set; }
    public IFormFile? Img { get; set; }
    public string? Status { get; set; }
    public long CategoryId { get; set; }

    public ItemForm()
    {
    }

    public ItemForm(long itemId, string itemName, int? price, int? quantity, IFormFile img, string status, long categoryId)
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