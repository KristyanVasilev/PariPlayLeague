using PariPlayLeague.Application.Features.Seasons.Commands;

namespace PariPlayLeague.API.Contracts.Requests.Seasons
{
    public record CreateSeasonRequest
    {
        public string Name { get; init; } = default!;
        public HashSet<Guid> TeamsIds { get; init; } = default!;

        public static CreateSeasonCommand MapToCommand(CreateSeasonRequest request)
        {
            return new CreateSeasonCommand
            {
                Name = request.Name,
                TeamsIds = request.TeamsIds
            };
        }
    }
}
