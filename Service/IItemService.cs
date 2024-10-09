using market.Domain;

namespace market.Service;

public interface IItemService
{
    Item SaveItem(Item item);
    IEnumerable<ItemDto> FindItemsAll(int page, int pageSize, SearchCon? searchCon);
    IEnumerable<Item> FindByCat(Category category);
    Item? FindById(long id);
    void DeleteById(long id);
}