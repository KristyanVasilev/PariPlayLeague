using PariPlayLeague.Domain.Entities;

namespace PariPlayLeague.API.Contracts.Responses.Seasons
{
    public record CreateSeasonResponse
    {
        public string Name { get; init; } = default!;
        public DateTime StartDate { get; init; } = default!;
        public List<SeasonTeamResponse> Teams { get; init; } = default!;

        public static CreateSeasonResponse MapToResponse(Season season)
        {
            return new CreateSeasonResponse
            {
                Name = season.Name,
                StartDate = season.StartDate,
                Teams = season.TeamSeasons.Select(SeasonTeamResponse.MapToResponse).ToList()
            };
        }
    }
}
