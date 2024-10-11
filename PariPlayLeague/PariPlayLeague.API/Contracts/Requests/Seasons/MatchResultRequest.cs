using PariPlayLeague.Application.Features.Seasons.Commands;

namespace PariPlayLeague.API.Contracts.Requests.Seasons
{
    public record MatchResultRequest
    {
        public Guid Id { get; init; } = default!;
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }

        public static UpdateMatchResultCommand MapToCommand(MatchResultRequest request)
        {
            return new UpdateMatchResultCommand
            {
                Id = request.Id,
                HomeTeamGoals = request.HomeTeamGoals,
                AwayTeamGoals = request.AwayTeamGoals,
            };
        }
    }
}
