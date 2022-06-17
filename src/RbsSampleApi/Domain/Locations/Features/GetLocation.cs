namespace RbsSampleApi.Domain.Locations.Features;

using RBSSample.Shared.Dtos.Location;
using AutoMapper;
using MediatR;
using RbsSampleApi.Databases;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using RbsSampleApi.Exceptions;

public static class GetLocation
{
    public class LocationQuery : IRequest<LocationDto>
    {
        public readonly Guid Id;
        public string[] Includes { get; }

        public LocationQuery(Guid id, string[] includes)
        {
            Id = id;
            Includes = includes;
        }
    }

    public class Handler : IRequestHandler<LocationQuery, LocationDto>
    {
        private readonly RbsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(RbsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<LocationDto> Handle(LocationQuery request, CancellationToken cancellationToken)
        {
            var includes = request.Includes;



            var result = await _db.Locations
                .AsNoTracking()
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider, null, includes)
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Location", request.Id);

            return result;
        }
    }
}