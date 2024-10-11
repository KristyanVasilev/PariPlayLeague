using PariPlayLeague.Application.Features.Teams.Commands;

namespace PariPlayLeague.API.Contracts.Requests.Teams
{
    public record CreateTeamRequest
    {
        public string Name { get; init; } = default!;
        public string President { get; init; } = default!;

        public static CreateTeamCommand MapToCommand(CreateTeamRequest request)
        {
            return new CreateTeamCommand
            {
                Name = request.Name,
                President = request.President,
            };
        }
    }
}
