namespace RBSSample.Shared.Dtos.Location;

using RBSSample.Shared.Dtos.Shared;

public class LocationParametersDto : BasePaginationParameters
{
    public string Filters { get; set; } = string.Empty;
    public string SortOrder { get; set; } = "Key";
    public string[] Includes { get; set; } = Array.Empty<string>();
}