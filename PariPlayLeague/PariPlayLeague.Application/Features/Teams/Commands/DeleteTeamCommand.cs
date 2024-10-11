using MediatR;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Domain.Entities;
using PariPlayLeague.Domain.Exceptions;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Teams.Commands
{
    public class DeleteTeamCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public DeleteTeamCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Result<Guid>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public DeleteTeamCommandHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _context
                .Teams
                .FirstOrDefaultAsync(entity => request.Id.Equals(entity.Id), cancellationToken)
                ?? throw new NotFoundException($"Team with id - {request.Id} not found!");

            _context.Set<Team>().Remove(team);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult<Guid>(request.Id)
            {
                Message = $"Team was deleted successfully with id - {request.Id}"
            };
        }
    }
}
