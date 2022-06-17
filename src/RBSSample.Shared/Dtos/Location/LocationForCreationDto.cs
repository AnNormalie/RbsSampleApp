namespace RBSSample.Shared.Dtos.Location;
public class LocationForCreationDto
{
    public string Key { get; set; } = string.Empty;
    public int LanguageId { get; set; }
    public string Information { get; set; } = string.Empty;
    public bool Exist { get; set; }
}