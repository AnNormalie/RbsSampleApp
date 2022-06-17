namespace RbsSampleApi.Domain.Languages.Features;

using RBSSample.Shared.Dtos.Language;

using RbsSampleApi.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using RbsSampleApi.Databases;

public static class GetLanguageList
{
    public class LanguageListQuery : IRequest<PagedLanguageResultDto>
    {
        public readonly LanguageParametersDto QueryParameters;

        public LanguageListQuery(LanguageParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<LanguageListQuery, PagedLanguageResultDto>
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

        public async Task<PagedLanguageResultDto> Handle(LanguageListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Languages
                as IQueryable<Language>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var includes = request.QueryParameters.Includes;



            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<LanguageDto>(_mapper.ConfigurationProvider, null, includes);

            var pagedList = await PagedList<LanguageDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);

            return new PagedLanguageResultDto
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
                Languages = pagedList
            };
        }
    }
}