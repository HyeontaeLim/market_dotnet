using market.Domain;
using market.Filter;
using market.Service;
using market.Validation;
using Microsoft.AspNetCore.Mvc;
using static System.IO.File;

namespace market.Controller;

[Route("[controller]")]
public class ItemController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IItemService _itemService;
    private readonly IMemberService _memberService;
    private readonly ILogger<ItemController> _logger;
    private readonly FileStore _fileStore;

    public ItemController(IItemService itemService, FileStore fileStore, ILogger<ItemController> logger, IMemberService memberService)
    {
        _itemService = itemService;
        _fileStore = fileStore;
        _logger = logger;
        _memberService = memberService;
    }

    [HttpPost("/item"), Consumes("multipart/form-data")]
    public IActionResult ItemSave(ItemForm itemForm)
    {
        if (!ModelState.IsValid)
        {
            var validationResult = new ValidationResult(ModelState, itemForm);
            return BadRequest(validationResult);
        }
        
        var uploadFile = _fileStore.StoreFile(itemForm.Img);
        var storeFileName = uploadFile.StoreFileName;
        bool status = itemForm.Status == "on";
        Item item = new Item
        {
            ItemName = itemForm.ItemName,
            Price = (int)itemForm.Price!,
            Quantity = (int)itemForm.Quantity!,
            Img = _fileStore.GetServedPath(storeFileName),
            Status = status,
            CategoryId = itemForm.CategoryId
        };
        _itemService.SaveItem(item);
        return Redirect("/");
    }
    
    [HttpGet("/items")]
    [ServiceFilter(typeof(LoginFilter))]
    public IEnumerable<ItemDto> FindItemsAll(int page, int pageSize, SearchCon? searchCon)
    {
        _logger.LogInformation(searchCon.ToString());
        if (searchCon.CategoryId != null)
        {
            _logger.LogInformation(string.Join(", ", searchCon.CategoryId!));
        }
        return _itemService.FindItemsAll(page, pageSize, searchCon);
    }
    
    [HttpGet("/item/{id}")]
    [ServiceFilter(typeof(LoginFilter))]
    public IActionResult FindById(long id)
    {
        var foundItem = _itemService.FindById(id);
        return Ok(foundItem);
    }


    [HttpDelete("/item/{id}")]
    [ServiceFilter(typeof(LoginFilter))]
    public void DeleteItem(long id)
    {
        var foundItem = _itemService.FindById(id);
        if (foundItem == null)
        {
            Console.WriteLine("no item found");
        }
        if(Exists("/Users/hyeontaelim/RiderProjects/market/market/wwwroot"+foundItem.Img) && foundItem.Img != "/images/no_image.jpg")
        {
            Delete("/Users/hyeontaelim/RiderProjects/market/market/wwwroot"+foundItem.Img);
        }
        _itemService.DeleteById(id);
    }
}