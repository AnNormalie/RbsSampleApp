namespace RBSSample.Shared.Dtos.Location;

using RBSSample.Shared.Dtos.Language;
using System.Collections.Generic;

public class LocationDto
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public int LanguageId { get; set; }
    public string Information { get; set; }
    public bool Exist { get; set; }
    public LanguageDto Language { get; set; }

}