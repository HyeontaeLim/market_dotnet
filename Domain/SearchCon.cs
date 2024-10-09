namespace market.Domain;

public class SearchCon
{
    public bool Status { get; set; }
    public string? Keyword { get; set; }
    public HashSet<long>? CategoryId { get; set; }

    public SearchCon(bool status, string? keyword, HashSet<long>? categoryId)
    {
        Status = status;
        Keyword = keyword;
        CategoryId = categoryId;
    }

    public SearchCon()
    {
    }

    public override string ToString()
    {
        return $"status: {Status}, keyword: {Keyword}, categoryId: {CategoryId}";
    }
}