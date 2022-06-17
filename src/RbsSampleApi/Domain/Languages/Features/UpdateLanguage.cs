namespace RbsSampleApi.Domain.Languages.Features;

using RbsSampleApi.Domain.Languages;
using RBSSample.Shared.Dtos.Language;
using RbsSampleApi.Domain.Languages.Validators;
using RbsSampleApi.Services;
using AutoMapper;
using MediatR;
using RbsSampleApi.Databases;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RbsSampleApi.Exceptions;

public static class UpdateLanguage
{
    public class UpdateLanguageCommand : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly LanguageForUpdateDto LanguageToUpdate;

        public UpdateLanguageCommand(Guid language, LanguageForUpdateDto newLanguageData)
        {
            Id = language;
            LanguageToUpdate = newLanguageData;
        }
    }

    public class Handler : IRequestHandler<UpdateLanguageCommand, bool>
    {
        private readonly RbsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(RbsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var languageToUpdate = await _db.Languages
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (languageToUpdate == null)
                throw new NotFoundException("Language", request.Id);

            languageToUpdate.Update(request.LanguageToUpdate);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}