using RBSSample.Shared.Dtos.Another;
using RBSSample.Shared.Dtos.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSSample.Shared.Dtos.SomeOther;

public class SomeOtherDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<LocationDto>? Locations { get; set; }
    public virtual ICollection<AnotherDto>? Anothers { get; set; }
}