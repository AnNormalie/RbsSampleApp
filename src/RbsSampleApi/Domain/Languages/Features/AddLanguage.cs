namespace RbsSampleApi.Domain.Languages.Features;


using RbsSampleApi.Domain.Languages;
using RbsSampleApi.Services;
using AutoMapper;
using MediatR;
using RBSSample.Shared.Dtos.Language;
using RbsSampleApi.Databases;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

public static class AddLanguage
{
    public class AddLanguageCommand : IRequest<LanguageDto>
    {
        public readonly LanguageForCreationDto LanguageToAdd;

        public AddLanguageCommand(LanguageForCreationDto languageToAdd)
        {
            LanguageToAdd = languageToAdd;
        }
    }

    public class Handler : IRequestHandler<AddLanguageCommand, LanguageDto>
    {
        private readonly RbsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(RbsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<LanguageDto> Handle(AddLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = Language.Create(request.LanguageToAdd);
            _db.Languages.Add(language);

            await _db.SaveChangesAsync(cancellationToken);

            return await _db.Languages
                .AsNoTracking()
                .ProjectTo<LanguageDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(l => l.Id == language.Id, cancellationToken);
        }
    }
}