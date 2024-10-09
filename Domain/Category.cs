namespace market.Domain;

public class Category
{
    public long? CategoryId { get; set; }
    public string? CatName { get; set; }

    public Category(long categoryId, string catName)
    {
        CategoryId = categoryId;
        CatName = catName;
    }
}