using market.Domain;

namespace market.Repository;

public interface ICategoryRepository
{
    Category? FindById(long id);
    IEnumerable<Category> FindAllCategory(string catName);
}