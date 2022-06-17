namespace RbsSampleApi.Domain.Languages.Features;

using RBSSample.Shared.Dtos.Language;
using AutoMapper;
using MediatR;
using RbsSampleApi.Databases;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using RbsSampleApi.Exceptions;

public static class GetLanguage
{
    public class LanguageQuery : IRequest<LanguageDto>
    {
        public readonly Guid Id;
        public string[] Includes { get; set; }

        public LanguageQuery(Guid id, string[] includes)
        {
            Id = id;
            Includes = includes;
        }
    }

    public class Handler : IRequestHandler<LanguageQuery, LanguageDto>
    {
        private readonly RbsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(RbsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<LanguageDto> Handle(LanguageQuery request, CancellationToken cancellationToken)
        {
            var includes = request.Includes;



            var result = await _db.Languages
                .AsNoTracking()
                .ProjectTo<LanguageDto>(_mapper.ConfigurationProvider, null, includes)
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Language", request.Id);

            return result;
        }
    }
}