using PariPlayLeague.Domain.Entities;

namespace PariPlayLeague.API.Contracts.Responses.Teams
{
    public record CreateTeamResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string President { get; init; } = default!;
        public DateTime CreatedOn { get; init; }

        public static CreateTeamResponse MapToResponse(Team team)
        {
            return new CreateTeamResponse
            {
                Id = team.Id,
                Name = team.Name,
                President = team.President,
                CreatedOn = team.CreatedOn,
            };
        }
    }
}
