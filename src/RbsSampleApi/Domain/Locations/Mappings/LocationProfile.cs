namespace RbsSampleApi.Domain.Locations.Mappings;

using RBSSample.Shared.Dtos.Location;
using AutoMapper;
using RbsSampleApi.Domain.Locations;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        //createmap<to this, from this>
        CreateMap<Location, LocationDto>()
            .ReverseMap();
        CreateMap<LocationForCreationDto, Location>();
        CreateMap<LocationForUpdateDto, Location>()
            .ReverseMap();
    }
}