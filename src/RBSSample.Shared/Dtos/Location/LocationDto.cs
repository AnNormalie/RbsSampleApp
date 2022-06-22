namespace RBSSample.Shared.Dtos.Location;

using RBSSample.Shared.Dtos.Another;
using RBSSample.Shared.Dtos.Language;
using RBSSample.Shared.Dtos.SomeOther;
using System.Collections.Generic;

public class LocationDto
{
    public Guid Id { get; set; }
    public string Key { get; set; }
    public Guid LanguageId { get; set; }
    public Guid SomeOtherId { get; set; }
    public string Information { get; set; }
    public bool Exist { get; set; }
    public LanguageDto Language { get; set; }
    public SomeOtherDto SomeOther { get; set; }
    public virtual ICollection<AnotherDto>? Anothers { get; set; }

}