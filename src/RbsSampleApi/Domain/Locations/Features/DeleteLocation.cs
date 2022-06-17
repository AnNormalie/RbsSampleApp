namespace RbsSampleApi.Domain.Locations.Features;

using RbsSampleApi.Services;
using MediatR;
using RbsSampleApi.Databases;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RbsSampleApi.Exceptions;

public static class DeleteLocation
{
    public class DeleteLocationCommand : IRequest<bool>
    {
        public readonly Guid Id;

        public DeleteLocationCommand(Guid location)
        {
            Id = location;
        }
    }

    public class Handler : IRequestHandler<DeleteLocationCommand, bool>
    {
        private readonly RbsDbContext _db;
        private readonly IMapper _mapper;

        public Handler(RbsDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.Locations
                .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("Location", request.Id);

            _db.Locations.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}