using PariPlayLeague.Application.Features.Seasons;

namespace PariPlayLeague.API.Contracts.Responses.Seasons
{
    public record GetPastMatchesResponse
    {
        public Guid Id { get; init; }
        public string Date { get; set; } = default!;
        public Guid HomeTeamId { get; init; } = default!;
        public string HomeTeamName { get; init; } = default!;
        public int HomeTeamGoals { get; init; } = default!;
        public Guid AwayTeamId { get; init; } = default!;
        public string AwayTeamName { get; init; } = default!;
        public int AwayTeamGoals { get; init; }

        public static PastMatchesDTOResponse MapToResponse(PastMatchesDto matches)
        {
            return new PastMatchesDTOResponse
            {
                TotalPastMatches = matches.PastMatchesCount,
                Matches = matches.Matches.Select(match => new GetPastMatchesResponse
                {
                    Id = match.Id,
                    Date = match.Date.Hour != 10 || match.Date.Minute != 8
                           ? match.Date.ToString("dd/MM/yyyy HH:mm")
                           : match.Date.ToString("dd/MM/yyyy"),
                    HomeTeamId = match.HomeTeamId,
                    HomeTeamName = match.HomeTeam.Name,
                    HomeTeamGoals = match.GoalsHomeTeam,
                    AwayTeamId = match.AwayTeamId,
                    AwayTeamName = match.AwayTeam.Name,
                    AwayTeamGoals = match.GoalsAwayTeam,
                }).ToList()
            };
        }
    }
    public class PastMatchesDTOResponse
    {
        public int TotalPastMatches { get; set; }
        public List<GetPastMatchesResponse> Matches { get; set; } = new List<GetPastMatchesResponse>();
    }
}
