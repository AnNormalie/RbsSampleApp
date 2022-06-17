namespace RBSSample.Shared.Dtos.Location;
public class LocationForUpdateDto
{
    public int LanguageId { get; set; }
    public string Information { get; set; } = string.Empty;
    public bool Exist { get; set; }
}