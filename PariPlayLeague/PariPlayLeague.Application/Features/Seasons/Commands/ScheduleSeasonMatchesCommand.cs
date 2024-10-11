using MediatR;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Domain.Entities;
using PariPlayLeague.Domain.Exceptions;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Seasons.Commands
{
    public class ScheduleSeasonMatchesCommand : IRequest<Result<string>>
    {
        public Guid SeasonId { get; set; }
        public ScheduleSeasonMatchesCommand(Guid seasonId)
        {
            SeasonId = seasonId;
        }
    }

    public class ScheduleSeasonMatchesCommandHandler : IRequestHandler<ScheduleSeasonMatchesCommand, Result<string>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public ScheduleSeasonMatchesCommandHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(ScheduleSeasonMatchesCommand request, CancellationToken cancellationToken)
        {
            var season = await _context.Seasons
                                       .Include(s => s.TeamSeasons)
                                       .ThenInclude(ts => ts.Team)
                                       .FirstOrDefaultAsync(s => s.Id == request.SeasonId, cancellationToken)
                                       ?? throw new NotFoundException($"Season with provided id - {request.SeasonId} not found");

            var teams = season.TeamSeasons.Select(ts => ts.Team).ToList();

            int n = teams.Count();
            bool isOdd = n % 2 != 0;

            // Add a dummy team "Dummy" team if the number of teams is odd
            if (isOdd)
            {
                teams.Add(new Team { Name = "Dummy" });
                n++;
            }

            var totalRounds = n - 1;
            var matchesPerRound = n / 2;

            List<Match> matchSchedule = new List<Match>();

            for (int round = 0; round < totalRounds; round++)
            {
                for (int i = 0; i < matchesPerRound; i++)
                {
                    int homeIndex = (round + i) % (n - 1);
                    int awayIndex = (n - 1 - i + round) % (n - 1);

                    if (i == 0)
                    {
                        awayIndex = n - 1;
                    }

                    var homeTeam = teams[homeIndex];
                    var awayTeam = teams[awayIndex];

                    // Skip "Dummy" matches
                    if (homeTeam.Name == "Dummy" || awayTeam.Name == "Dummy")
                        continue;

                    matchSchedule.Add(new Match
                    {
                        Date = season.StartDate.AddDays(round * 7),
                        HomeTeamId = homeTeam.Id,
                        HomeTeam = homeTeam,
                        AwayTeamId = awayTeam.Id,
                        AwayTeam = awayTeam
                    });
                }
            }

            List<Match> fullSchedule = new List<Match>(matchSchedule);

            foreach (var match in matchSchedule)
            {
                fullSchedule.Add(new Match
                {
                    Date = match.Date.AddDays(7 * totalRounds),
                    HomeTeamId = match.AwayTeamId,
                    HomeTeam = match.AwayTeam,
                    AwayTeamId = match.HomeTeamId,
                    AwayTeam = match.HomeTeam
                });
            }

            season.Matches.AddRange(fullSchedule);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult<string>($"{fullSchedule.Count()} matches scheduled and saved successfully.");
        }
    }
}
