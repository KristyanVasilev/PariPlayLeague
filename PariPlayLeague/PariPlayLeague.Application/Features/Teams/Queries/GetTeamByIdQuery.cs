using MediatR;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Domain.Entities;
using PariPlayLeague.Domain.Exceptions;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Teams.Queries
{
    public class GetTeamByIdQuery : IRequest<Result<Team>>
    {
        public Guid Id { get; set; }
        public GetTeamByIdQuery(Guid id)
        {
            Id = id;
        }
    }
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Result<Team>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public GetTeamByIdQueryHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Team>> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var team = await _context.Teams
                                     .AsNoTracking()
                                     .Include(t => t.TeamSeasons)
                                     .ThenInclude(ts => ts.Season)
                                     .Include(t => t.TeamSeasons)
                                     .ThenInclude(ts => ts.TeamStatistic)
                                     .FirstOrDefaultAsync(t => t.Id.Equals(request.Id), cancellationToken)
                                     ?? throw new NotFoundException($"No team found with that id - {request.Id}");

            return new SuccessResult<Team>(team);
        }
    }
}
