using MediatR;
using Microsoft.EntityFrameworkCore;
using PariPlayLeague.Application.ResultPattern;
using PariPlayLeague.Application.ResultPattern.SuccessResults;
using PariPlayLeague.Domain.Entities;
using PariPlayLeague.Domain.Exceptions;
using PariPlayLeague.Infrastructure;

namespace PariPlayLeague.Application.Features.Seasons.Commands
{
    public class CreateSeasonCommand : IRequest<Result<Season>>
    {
        public string Name { get; set; } = default!;
        public HashSet<Guid> TeamsIds { get; set; } = default!;
    }

    public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, Result<Season>>
    {
        private readonly PariPlayLeagueDbContext _context;
        public CreateSeasonCommandHandler(PariPlayLeagueDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Season>> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
        {
            var season = new Season { Name = request.Name, StartDate = DateTime.Now };

            var teams = await _context.Teams
                                      .Where(t => request.TeamsIds
                                      .Contains(t.Id) && t.IsDeleted == false)
                                      .ToListAsync(cancellationToken);

            if (!teams.Any())
            {
                var missingIds = string.Join(", ", request.TeamsIds);
                throw new NotFoundException($"No Teams found with the provided IDs - {missingIds}");
            }

            foreach (var team in teams)
            {
                var teamSeason = new TeamSeason
                {
                    Team = team,
                    Season = season,
                    TeamStatistic = new TeamStatistic()
                };

                season.TeamSeasons.Add(teamSeason);
                team.TeamSeasons.Add(teamSeason);
            }

            await _context.AddAsync(season, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new SuccessResult<Season>(season);
        }
    }
}
