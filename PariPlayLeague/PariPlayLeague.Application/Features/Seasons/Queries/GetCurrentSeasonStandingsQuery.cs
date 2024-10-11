using MediatR;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Seasons.Queries
{
    public class GetCurrentSeasonStandins : IRequest<Result<List<DTOStandings>>>
    {
    }
    public class GetCurrentSeasonStandinsHandler : IRequestHandler<GetCurrentSeasonStandins, Result<List<DTOStandings>>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public GetCurrentSeasonStandinsHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<DTOStandings>>> Handle(GetCurrentSeasonStandins request, CancellationToken cancellationToken)
        {
            var currentSeason = await _context.Seasons
                                              .FirstOrDefaultAsync(s => s.IsActive && !s.IsDeleted)
                                              ?? throw new Exception("No active season found.");


            var teamStandings = await _context.TeamsSeasons
                .Where(ts => ts.SeasonId == currentSeason.Id)
                .Select(ts => new DTOStandings
                {
                    TeamId = ts.TeamId,
                    TeamName = ts.Team.Name,

                    ScoredGoals = ts.TeamStatistic.ScoredGoals,
                    ConsidedGoals = ts.TeamStatistic.ConsidedGoals,
                    GoalDifference = ts.TeamStatistic.ScoredGoals - ts.TeamStatistic.ConsidedGoals,

                    Matches = ts.TeamStatistic.Matches,
                    WonMatches = ts.TeamStatistic.WonMatches,
                    DrawMatches = ts.TeamStatistic.DrawMatches,
                    LostMatches = ts.TeamStatistic.LostMatches,

                    Points = ts.TeamStatistic.WonMatches * 3 + ts.TeamStatistic.DrawMatches
                })
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.ScoredGoals)
                .ToListAsync();

            return new SuccessResult<List<DTOStandings>>(teamStandings);
        }
    }
}
public record DTOStandings
{
    public Guid TeamId { get; init; }
    public string TeamName { get; init; } = default!;

    public int Matches { get; init; }
    public int WonMatches { get; init; }
    public int DrawMatches { get; init; }
    public int LostMatches { get; init; }

    public int ScoredGoals { get; init; }
    public int ConsidedGoals { get; init; }
    public int GoalDifference { get; init; }

    public int Points { get; init; }
}
