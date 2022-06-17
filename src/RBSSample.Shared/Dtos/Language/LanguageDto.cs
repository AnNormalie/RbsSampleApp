namespace RBSSample.Shared.Dtos.Language;

using RBSSample.Shared.Dtos.Location;
using System.Collections.Generic;

public class LanguageDto
{
    public Guid Id { get; set; }
    public string Tag { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public virtual ICollection<LocationDto>? Locations { get; set; }
}