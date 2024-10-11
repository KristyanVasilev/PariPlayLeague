using PariPlayLeague.Domain.Entities;

namespace PariPlayLeague.API.Contracts.Responses.Seasons
{
    public record SeasonTeamResponse
    {
        public Guid TeamId { get; set; } = default!;
        public string TeamName { get; set; } = default!;

        public static SeasonTeamResponse MapToResponse(TeamSeason teamSeason)
        {
            return new SeasonTeamResponse
            {
                TeamId = teamSeason.TeamId,
                TeamName = teamSeason.Team.Name
            };
        }
    }
}
