using RBSSample.Shared.Dtos.Location;
using RBSSample.Shared.Dtos.SomeOther;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSSample.Shared.Dtos.Another;

public class AnotherDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SomeOtherId { get; set; }
    public Guid LocationId { get; set; }

    public SomeOtherDto SomeOther { get; set; }
    public LocationDto Location { get; set; }
}