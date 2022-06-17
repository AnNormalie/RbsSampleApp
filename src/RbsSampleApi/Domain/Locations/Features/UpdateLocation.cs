namespace RbsSampleApi.Domain.Locations.Features;

using RbsSampleApi.Domain.Locations;
using RBSSample.Shared.Dtos.Location;
using RbsSampleApi.Domain.Locations.Validators;
using RbsSampleApi.Services;
using AutoMapper;
using MediatR;
using RbsSampleApi.Databases;
using Microsoft.EntityFrameworkCore;
using RbsSampleApi.Exceptions;

public static class UpdateLocation
{
    public class UpdateLocationCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly LocationForUpdateDto LocationToUpdate;

        public UpdateLocationCommand(Guid location, LocationForUpdateDto newLocationData)
        {
            Id = location;
            LocationToUpdate = newLocationData;
        }
    }

    public class Handler : IRequestHandler<UpdateLocationCommand, bool>
    {
        private readonly RbsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(RbsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var locationToUpdate = await _db.Locations
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (locationToUpdate == null)
                throw new NotFoundException("Location", request.Id);

            locationToUpdate.Update(request.LocationToUpdate);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}