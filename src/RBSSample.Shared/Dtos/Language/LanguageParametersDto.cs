namespace RBSSample.Shared.Dtos.Language;

using RBSSample.Shared.Dtos.Shared;

public class LanguageParametersDto : BasePaginationParameters
{
    public string Filters { get; set; } = string.Empty;
    public string SortOrder { get; set; } = "Tag";
    public string[] Includes { get; set; } = Array.Empty<string>();

}