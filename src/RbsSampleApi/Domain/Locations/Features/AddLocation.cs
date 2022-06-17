namespace RbsSampleApi.Domain.Locations.Features;

using RbsSampleApi.Domain.Locations;
using RBSSample.Shared.Dtos.Location;
using RbsSampleApi.Services;
using AutoMapper;
using MediatR;
using RbsSampleApi.Databases;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

public static class AddLocation
{
    public class AddLocationCommand : IRequest<LocationDto>
    {
        public readonly LocationForCreationDto LocationToAdd;

        public AddLocationCommand(LocationForCreationDto locationToAdd)
        {
            LocationToAdd = locationToAdd;
        }
    }

    public class Handler : IRequestHandler<AddLocationCommand, LocationDto>
    {
        private readonly RbsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(RbsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<LocationDto> Handle(AddLocationCommand request, CancellationToken cancellationToken)
        {
            var location = Location.Create(request.LocationToAdd);
            _db.Locations.Add(location);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.Locations
                .AsNoTracking()
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(l => l.Id == location.Id, cancellationToken);
        }
    }
}