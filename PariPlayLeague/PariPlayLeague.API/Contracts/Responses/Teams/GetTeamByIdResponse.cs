using PariPlayLeague.Domain.Entities;

namespace PariPlayLeague.API.Contracts.Responses.Teams
{
    public record GetTeamByIdResponse
    {
        public string Name { get; init; } = default!;
        public string President { get; init; } = default!;
        public int Champion { get; init; }
        public string CreatedOn { get; init; } = default!;

        public TeamTotalStatsResponse TotalStats { get; set; } = default!;

        public static GetTeamByIdResponse MapToResponse(Team team)
        {
            var totalStats = new TeamTotalStatsResponse
            {
                Matches = team.TeamSeasons.Sum(ts => ts.TeamStatistic.Matches),
                ScoredGoals = team.TeamSeasons.Sum(ts => ts.TeamStatistic.ScoredGoals),
                Fouls = team.TeamSeasons.Sum(ts => ts.TeamStatistic.Fouls),
                YellowCards = team.TeamSeasons.Sum(ts => ts.TeamStatistic.YellowCards),
                RedCards = team.TeamSeasons.Sum(ts => ts.TeamStatistic.RedCards),
                ConsidedGoals = team.TeamSeasons.Sum(ts => ts.TeamStatistic.ConsidedGoals),
            };

            return new GetTeamByIdResponse
            {
                Name = team.Name,
                President = team.President,
                Champion = team.Champion,
                CreatedOn = team.CreatedOn.ToString("dd/MM/yyyy"),
                TotalStats = totalStats
            };
        }
    }
}
