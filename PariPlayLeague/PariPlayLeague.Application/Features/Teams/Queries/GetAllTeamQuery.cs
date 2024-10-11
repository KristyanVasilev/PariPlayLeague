using AutoFilterer.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.Filters;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Domain.Entities;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Teams.Queries
{
    public class GetAllTeamQuery : IRequest<Result<List<Team>>>
    {
        public GetAllTeamsFilter Filter { get; set; } = default!;
    }

    public class GetAllTeamQueryHandler : IRequestHandler<GetAllTeamQuery, Result<List<Team>>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public GetAllTeamQueryHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<Team>>> Handle(GetAllTeamQuery request, CancellationToken cancellationToken)
        {
            var teams = await _context.Teams
                                      .AsNoTracking()
                                      .OrderBy(t => t.Name)
                                      .ApplyFilter(request.Filter)
                                      .ToListAsync(cancellationToken);

            return new SuccessResult<List<Team>>(teams);
        }
    }
}
