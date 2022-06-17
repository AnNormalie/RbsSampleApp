namespace RbsSampleApi.Domain.Languages.Features;

using RbsSampleApi.Services;
using MediatR;
using RbsSampleApi.Databases;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RbsSampleApi.Exceptions;

public static class DeleteLanguage
{
    public class DeleteLanguageCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteLanguageCommand(Guid language)
        {
            Id = language;
        }
    }

    public class Handler : IRequestHandler<DeleteLanguageCommand, bool>
    {
        private readonly RbsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(RbsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.Languages
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("Language", request.Id);

            _db.Languages.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}