using PariPlayLeague.Application.Features.Seasons;

namespace PariPlayLeague.API.Contracts.Responses.Seasons
{
    public record GetNextMatchesResponse
    {
        public Guid Id { get; init; }
        public string Date { get; set; } = default!;
        public Guid HomeTeamId { get; init; } = default!;
        public string HomeTeamName { get; init; } = default!;
        public Guid AwayTeamId { get; init; } = default!;
        public string AwayTeamName { get; init; } = default!;

        public static NextMatchesDTOResponse MapToResponse(NextMatchesDto matches)
        {
            return new NextMatchesDTOResponse
            {
                TotalNextMatches = matches.NextMatchesCount,
                Matches = matches.Matches.Select(match => new GetNextMatchesResponse
                {
                    Id = match.Id,
                    Date = match.Date.Hour != 10 || match.Date.Minute != 8
                           ? match.Date.ToString("dd/MM/yyyy HH:mm")
                           : match.Date.ToString("dd/MM/yyyy"),
                    HomeTeamId = match.HomeTeamId,
                    HomeTeamName = match.HomeTeam.Name,
                    AwayTeamId = match.AwayTeamId,
                    AwayTeamName = match.AwayTeam.Name,
                }).ToList()
            };
        }
    }
    public class NextMatchesDTOResponse
    {
        public int TotalNextMatches { get; set; }
        public List<GetNextMatchesResponse> Matches { get; set; } = new List<GetNextMatchesResponse>();
    }
}
