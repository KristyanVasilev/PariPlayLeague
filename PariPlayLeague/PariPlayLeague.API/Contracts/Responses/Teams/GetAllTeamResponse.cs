using PariPlayLeague.Domain.Entities;

namespace PariPlayLeague.API.Contracts.Responses.Teams
{
    public record GetAllTeamResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;

        public static List<GetAllTeamResponse> MapToResponse(List<Team> teams)
        {
            return teams.Select(team => new GetAllTeamResponse
            {
                Id = team.Id,
                Name = team.Name,
            }).ToList();
        }
    }
}
