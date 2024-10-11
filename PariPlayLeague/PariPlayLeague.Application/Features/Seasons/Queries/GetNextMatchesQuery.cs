using AutoFilterer.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.Filters;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Seasons.Queries
{
    public class GetNextMatchesQuery : IRequest<Result<NextMatchesDto>>
    {
        public GetMatchesFilter Filter { get; set; } = default!;
    }
    public class GetNextMatchesQueryHandler : IRequestHandler<GetNextMatchesQuery, Result<NextMatchesDto>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public GetNextMatchesQueryHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Result<NextMatchesDto>> Handle(GetNextMatchesQuery request, CancellationToken cancellationToken)
        {
            var nextMatches = await _context.Matches
                                            .AsNoTracking()
                                            .Include(m => m.HomeTeam)
                                            .Include(m => m.AwayTeam)
                                            .Where(m => m.Date.Date >= DateTime.Now.Date)
                                            .OrderBy(m => m.Date)
                                            .ApplyFilter(request.Filter)
                                            .ToListAsync(cancellationToken);

            var nextMatchesCount = await _context.Matches
                                                 .AsNoTracking()
                                                 .Where(m => m.Date >= DateTime.Now.Date)
                                                 .CountAsync(cancellationToken);

            var matchesDto = new NextMatchesDto
            {
                Matches = nextMatches,
                NextMatchesCount = nextMatchesCount,
            };

            return new SuccessResult<NextMatchesDto>(matchesDto);
        }
    }
}
