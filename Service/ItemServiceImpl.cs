using market.Domain;
using market.Repository;

namespace market.Service;

public class ItemServiceImpl : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemServiceImpl(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public Item SaveItem(Item item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "item object cannot be null");
        }
        return _itemRepository.SaveItem(item);
    }

    public IEnumerable<ItemDto> FindItemsAll(int page, int pageSize, SearchCon? searchCon)
    {
        return _itemRepository.FindItemsAll(page, pageSize, searchCon);
    }

    public IEnumerable<Item> FindByCat(Category category)
    {
        throw new NotImplementedException();
    }

    public Item? FindById(long id)
    {
        return _itemRepository.FindById(id);
    }

    public void DeleteById(long id)
    {
        _itemRepository.DeleteById(id);
    }
}