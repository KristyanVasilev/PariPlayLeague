using MediatR;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Domain.Entities;
using PariPlayLeague.Domain.Exceptions;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Seasons.Commands
{
    public class UpdateMatchResultCommand : IRequest<Result<string>>
    {
        public Guid Id { get; init; } = default!;
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
    }

    public class UpdateMatchResultCommandCommandHandler : IRequestHandler<UpdateMatchResultCommand, Result<string>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public UpdateMatchResultCommandCommandHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdateMatchResultCommand request, CancellationToken cancellationToken)
        {
            var match = await _context.Matches
                                      .Include(m => m.HomeTeam).ThenInclude(t => t.TeamSeasons).ThenInclude(ts => ts.TeamStatistic)
                                      .Include(m => m.AwayTeam).ThenInclude(t => t.TeamSeasons).ThenInclude(ts => ts.TeamStatistic)
                                      .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                                      ?? throw new Exception($"Match with id - {request.Id} not found");

            // Prevent editing future matches
            if (match.Date >= DateTime.Now.Date)
            {
                throw new BadRequestException("Cannot edit result for future matches");
            }

            // Retrieve the active season (assumes there's only one active season at a time)
            var activeSeason = await _context.Seasons.FirstOrDefaultAsync(s => s.IsActive, cancellationToken)
                ?? throw new Exception("Active season not found.");

   
            var homeTeamSeason = GetTeamSeasonForActiveSeason(match.HomeTeam, activeSeason.Id);
            var awayTeamSeason = GetTeamSeasonForActiveSeason(match.AwayTeam, activeSeason.Id);

            UpdateTeamStatistics(homeTeamSeason.TeamStatistic, awayTeamSeason.TeamStatistic, request.HomeTeamGoals, request.AwayTeamGoals);

            match.GoalsHomeTeam = request.HomeTeamGoals;
            match.GoalsAwayTeam = request.AwayTeamGoals;

            _context.Matches.Update(match);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult<string>("Match result and team stats updated successfully");
        }
        private TeamSeason GetTeamSeasonForActiveSeason(Team team, Guid seasonId)
        {
            var teamSeason = team.TeamSeasons.FirstOrDefault(ts => ts.SeasonId == seasonId)
                ?? throw new Exception($"{team.Name} season stats not found.");

            return teamSeason;
        }
        private void UpdateTeamStatistics(TeamStatistic homeTeamStats, TeamStatistic awayTeamStats, int homeTeamGoals, int awayTeamGoals)
        {
            homeTeamStats.ScoredGoals += homeTeamGoals;
            homeTeamStats.ConsidedGoals += awayTeamGoals;

            awayTeamStats.ScoredGoals += awayTeamGoals;
            awayTeamStats.ConsidedGoals += homeTeamGoals;

            if (homeTeamGoals > awayTeamGoals)
            {
                homeTeamStats.WonMatches += 1;
                awayTeamStats.LostMatches += 1;
            }
            else if (homeTeamGoals < awayTeamGoals)
            {
                awayTeamStats.WonMatches += 1;
                homeTeamStats.LostMatches += 1;
            }
            else
            {
                homeTeamStats.DrawMatches += 1;
                awayTeamStats.DrawMatches += 1;
            }

            homeTeamStats.Matches += 1;
            awayTeamStats.Matches += 1;
        }
    }
}
