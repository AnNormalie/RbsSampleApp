namespace RbsSampleApi.Domain.Locations.Features;

using RBSSample.Shared.Dtos.Location;
using RbsSampleApi.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using RbsSampleApi.Databases;

public static class GetLocationList
{
    public class LocationListQuery : IRequest<PagedLocationResultDto>
    {
        public readonly LocationParametersDto QueryParameters;

        public LocationListQuery(LocationParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<LocationListQuery, PagedLocationResultDto>
    {
        private readonly RbsDbContext _db;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(RbsDbContext db, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedLocationResultDto> Handle(LocationListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Locations
                as IQueryable<Location>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var includes = request.QueryParameters.Includes;



            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider, null, includes);

            var pagedList = await PagedList<LocationDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);

            return new PagedLocationResultDto
            {
                CurrentEndIndex = pagedList.CurrentEndIndex,
                CurrentStartIndex = pagedList.CurrentStartIndex,
                CurrentPageSize = pagedList.CurrentPageSize,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                PageNumber = pagedList.PageNumber,
                PageSize = pagedList.PageSize,
                TotalCount = pagedList.TotalCount,
                TotalPages = pagedList.TotalPages,
                Locations = pagedList
            };
        }
    }
}