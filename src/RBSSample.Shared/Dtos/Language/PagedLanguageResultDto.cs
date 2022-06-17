namespace RBSSample.Shared.Dtos.Language;

public class PagedLanguageResultDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPageSize { get; set; }
    public int CurrentStartIndex { get; set; }
    public int CurrentEndIndex { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public List<LanguageDto>? Languages { get; set; }
}
