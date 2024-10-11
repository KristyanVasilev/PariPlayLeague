using AutoFilterer.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.Filters;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Seasons.Queries
{
    public class GetPastMatchesQuery : IRequest<Result<PastMatchesDto>>
    {
        public GetMatchesFilter Filter { get; set; } = default!;
    }

    public class GetPastMatchesQueryHandler : IRequestHandler<GetPastMatchesQuery, Result<PastMatchesDto>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public GetPastMatchesQueryHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Result<PastMatchesDto>> Handle(GetPastMatchesQuery request, CancellationToken cancellationToken)
        {
            var pastMatches = await _context.Matches
                                            .AsNoTracking()
                                            .Include(m => m.HomeTeam)
                                            .Include(m => m.AwayTeam)
                                            .Where(m => m.Date.Date < DateTime.Now.Date)
                                            .OrderBy(m => m.Date)
                                            .ApplyFilter(request.Filter)
                                            .ToListAsync(cancellationToken);

            var pastMatchesCount = await _context.Matches
                                                 .AsNoTracking()
                                                 .Where(m => m.Date < DateTime.Now.Date)
                                                 .CountAsync(cancellationToken);

            var pastMatchesDto = new PastMatchesDto
            {
                Matches = pastMatches,
                PastMatchesCount = pastMatchesCount
            };

            return new SuccessResult<PastMatchesDto>(pastMatchesDto);
        }
    }
}
